﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Win32;
using Microsoft.Runtime;
using System.Runtime.InteropServices;


namespace dCover.Geral
{
	class ProjectProcess
	{
		const int EXCEPTION_DEBUG_EVENT = 1;
		const int CREATE_THREAD_DEBUG_EVENT = 2;
		const int CREATE_PROCESS_DEBUG_EVENT = 3;
		const int EXIT_THREAD_DEBUG_EVENT = 4;
		const int EXIT_PROCESS_DEBUG_EVENT = 5;
		const int LOAD_DLL_DEBUG_EVENT = 6;
		const int UNLOAD_DLL_DEBUG_EVENT = 7;
		const int OUTPUT_DEBUG_STRING_EVENT = 8;
		const int RIP_EVENT = 9;
		const uint STATUS_BREAKPOINT = 0x80000003;
		
		private Thread debuggingThread = null;
		STARTUPINFO startupInfo = new STARTUPINFO();
		PROCESS_INFORMATION processInformation = new PROCESS_INFORMATION();
		private ProjectModule module;

		public bool status { get{ return debuggingThread == null ? true : debuggingThread.IsAlive; } }




		void debuggingLoop()
		{
			CreateProcess(@"D:\Projetos\Dummy_Coverage\Project1.exe", null, 0, 0, false, 2, 0, null, ref startupInfo, out processInformation);
			
			uint continueStatus;
			
			while(status)
			{
				DEBUG_EVENT debugEvent = new DEBUG_EVENT();

				if(!WaitForDebugEvent(out debugEvent, 0xFFFFFFFF))
					continue;

				continueStatus = 0x00010002;
				
				switch(debugEvent.dwDebugEventCode)
				{
					case CREATE_PROCESS_DEBUG_EVENT:
						{
							break;
						}

					case EXCEPTION_DEBUG_EVENT:
						{
							if(debugEvent.Exception.ExceptionRecord.ExceptionCode != STATUS_BREAKPOINT)
							{
								continueStatus = 0x80010001;
								break;
							}
							//Handle breakpoint here
							break;
						}
				}

				ContinueDebugEvent(debugEvent.dwProcessId, debugEvent.dwThreadId, continueStatus);
			}
		}

		public bool CreateProcess(ProjectModule targetModule)
		{
			startupInfo.cb = (uint)System.Runtime.InteropServices.Marshal.SizeOf(startupInfo);
			startupInfo.dwFlags = 1;
			module = targetModule;

			debuggingThread = new Thread(new ThreadStart(debuggingLoop));
			debuggingThread.Start();
		
			ResumeThread(processInformation.hThread);
			return true;
		}

		[Flags]
		public enum HookRegister
		{
			None = 0,
			DR0 = 1,
			DR1 = 2,
			DR2 = 4,
			DR3 = 8
		}

		#region Structs


		[StructLayout(LayoutKind.Sequential)]
		private unsafe struct DEBUG_EVENT
		{
			public readonly uint dwDebugEventCode;
			public readonly uint dwProcessId;
			public readonly uint dwThreadId;


			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 86, ArraySubType = UnmanagedType.U1)]
			private readonly byte[] debugInfo;


			public EXCEPTION_DEBUG_INFO Exception
			{
				get
				{
					if (debugInfo == null)
						return new EXCEPTION_DEBUG_INFO();


					fixed (byte* ptr = debugInfo)
					{
						return *(EXCEPTION_DEBUG_INFO*)ptr;
					}
				}
			}


			public LOAD_DLL_DEBUG_INFO LoadDll
			{
				get
				{
					if (debugInfo == null)
						return new LOAD_DLL_DEBUG_INFO();


					fixed (byte* ptr = debugInfo)
					{
						return *(LOAD_DLL_DEBUG_INFO*)ptr;
					}
				}
			}
		}


		[StructLayout(LayoutKind.Sequential)]
		private struct EXCEPTION_DEBUG_INFO
		{
			public EXCEPTION_RECORD ExceptionRecord;
			public readonly uint dwFirstChance;
		}


		[StructLayout(LayoutKind.Sequential)]
		private struct EXCEPTION_RECORD
		{
			public readonly uint ExceptionCode;
			public readonly uint ExceptionFlags;
			public readonly uint ExceptionRecord;
			public readonly uint ExceptionAddress;
			public readonly uint NumberParameters;

			public unsafe fixed uint ExceptionInformation[15];
		}


		[StructLayout(LayoutKind.Sequential)]
		public struct FLOATING_SAVE_AREA
		{
			public uint ControlWord;
			public uint StatusWord;
			public uint TagWord;
			public uint ErrorOffset;
			public uint ErrorSelector;
			public uint DataOffset;
			public uint DataSelector;

			public unsafe fixed byte RegisterArea[80];


			public uint Cr0NpxState;
		}


		[StructLayout(LayoutKind.Sequential)]
		private struct LOAD_DLL_DEBUG_INFO
		{
			public readonly uint hFile;
			public readonly uint lpBaseOfDll;
			public readonly uint dwDebugInfoFileOffset;
			public readonly uint nDebugInfoSize;
			public readonly uint lpImageName;
			public readonly ushort fUnicode;
		}


		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
		private struct THREADENTRY32
		{
			internal UInt32 dwSize;
			internal readonly UInt32 cntUsage;
			internal readonly UInt32 th32ThreadID;
			internal readonly UInt32 th32OwnerProcessID;
			internal readonly UInt32 tpBasePri;
			internal readonly UInt32 tpDeltaPri;
			internal readonly UInt32 dwFlags;
		}


		[StructLayout(LayoutKind.Sequential)]
		public struct ThreadContext
		{
			public uint ContextFlags; //set this to an appropriate value 
			// Retrieved by CONTEXT_DEBUG_REGISTERS 
			public uint Dr0;
			public uint Dr1;
			public uint Dr2;
			public uint Dr3;
			public uint Dr6;
			public uint Dr7;
			// Retrieved by CONTEXT_FLOATING_POINT 
			public FLOATING_SAVE_AREA FloatSave;
			// Retrieved by CONTEXT_SEGMENTS 
			public uint SegGs;
			public uint SegFs;
			public uint SegEs;
			public uint SegDs;
			// Retrieved by CONTEXT_INTEGER 
			public uint Edi;
			public uint Esi;
			public uint Ebx;
			public uint Edx;
			public uint Ecx;
			public uint Eax;
			// Retrieved by CONTEXT_CONTROL 
			public uint Ebp;
			public uint Eip;
			public uint SegCs;
			public uint EFlags;
			public uint Esp;
			public uint SegSs;
			// Retrieved by CONTEXT_EXTENDED_REGISTERS 
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 512)]
			public byte[] ExtendedRegisters;
		}

		public struct PROCESS_INFORMATION
		{
			public uint hProcess;
			public uint hThread;
			public uint dwProcessId;
			public uint dwThreadId;
		}

		public struct STARTUPINFO
		{
			public uint cb;
			public string lpReserved;
			public string lpDesktop;
			public string lpTitle;
			public uint dwX;
			public uint dwY;
			public uint dwXSize;
			public uint dwYSize;
			public uint dwXCountChars;
			public uint dwYCountChars;
			public uint dwFillAttribute;
			public uint dwFlags;
			public short wShowWindow;
			public short cbReserved2;
			public uint lpReserved2;
			public uint hStdInput;
			public uint hStdOutput;
			public uint hStdError;
		}

		public struct SECURITY_ATTRIBUTES
		{
			public int length;
			public uint lpSecurityDescriptor;
			public bool bInheritHandle;
		}



		#endregion

		#region Imports

		[DllImport("kernel32.dll")]
		private static extern uint OpenThread(uint dwDesiredAccess, bool bInheritHandle,
			uint dwThreadId);

		[DllImport("kernel32.dll")]
		private static extern uint GetLastError();


		[DllImport("kernel32.dll")]
		private static extern uint OpenProcess(uint dwDesiredAccess, bool bInheritHandle,
			uint dwThreadId);


		[DllImport("kernel32.dll")]
		private static extern bool Thread32First(uint hSnapshot, ref THREADENTRY32 lpte);


		[DllImport("kernel32.dll")]
		private static extern bool Thread32Next(uint hSnapshot, out THREADENTRY32 lpte);


		[DllImport("kernel32.dll", SetLastError = true)]
		private static extern uint CreateToolhelp32Snapshot(int dwFlags, uint th32ProcessID);


		[DllImport("kernel32.dll")]
		private static extern uint SuspendThread(uint hThread);


		[DllImport("kernel32.dll")]
		private static extern bool GetThreadContext(uint hThread, ref ThreadContext lpContext);


		[DllImport("kernel32.dll")]
		private static extern bool SetThreadContext(uint hThread,
			[In] ref ThreadContext lpContext);


		[DllImport("kernel32.dll")]
		private static extern uint ResumeThread(uint hThread);


		[DllImport("kernel32.dll")]
		private static extern bool DebugActiveProcess(uint dwProcessId);


		[DllImport("kernel32.dll")]
		private static extern bool DebugSetProcessKillOnExit(uint dwProcessId);


		[DllImport("kernel32.dll", EntryPoint = "WaitForDebugEvent")]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool WaitForDebugEvent(out DEBUG_EVENT lpDebugEvent, uint dwMilliseconds);


		[DllImport("kernel32.dll")]
		private static extern bool ContinueDebugEvent(uint dwProcessId, uint dwThreadId,
			uint dwContinueStatus);


		[DllImport("kernel32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool CloseHandle(uint hObject);

		[DllImport("kernel32.dll")]
		static extern bool CreateProcess(string lpApplicationName, string lpCommandLine, uint lpProcessAttributes, uint lpThreadAttributes,
								bool bInheritHandles, uint dwCreationFlags, uint lpEnvironment,
								string lpCurrentDirectory, ref STARTUPINFO lpStartupInfo, out PROCESS_INFORMATION lpProcessInformation);
	}
	#endregion

	/*

	public class ExternalProcessHook : IDisposable
	{
		public delegate void HandleHookCallback(ref ThreadContext threadContext);


		private static int _hookCount;
		private static readonly List<HookItem> Hooks = new List<HookItem>();
		private static Thread _workerThread;
		private static bool _removeHook;
		private static bool _debugActiveAlready;
		private readonly HookItem _hook;
		private bool _disposed;


		public ExternalProcessHook(MemoryBase memory, HookRegister register, uint hookLocation,
			HandleHookCallback callback)
		{
			var i = new HookItem { Callback = callback, Location = hookLocation, Register = register };
			_hook = i;
			Hooks.Add(i);
			_hookCount++;


			// So basically, DR hooks work off "waiting for a debug event" basically.
			// In actuality we're waiting on an exception, but for the sake of wrapping a hook,
			// we'll do it in a separate thread. This means we need to ensure we close the thread (IsBackground) when the app closes
			// and ensure we only ever create *one* polling thread.
			if (_hookCount == 0 || _workerThread == null)
			{
				_workerThread =
					new Thread(() => InstallHardwareHook(memory.Process));
				_workerThread.IsBackground = true;
				_workerThread.Start();
			}
			SetThreadHook(i, false);
		}


		public void Dispose()
		{
			if (_disposed)
				return;
			_hookCount--;


			SetThreadHook(_hook, true);
			if (_hookCount == 0)
			{
				_removeHook = true;
			}
			// Remove ourselves from the hook list... we're done. :)
			Hooks.Remove(_hook);
			_disposed = true;
		}


		private static void OpenAllThreads(Process proc)
		{
			// This isn't super needed, it's just to OpenThread a ton of things and get handles for later.
			// Unfortunately, the .NET ProcessThread stuff isn't always accurate, so we'll just skip it
			// entirely and do it the native win32 way.
			var te = new THREADENTRY32();
			te.dwSize = 28; // sizeof(THREADENTRY32)


			uint hSnapshot = CreateToolhelp32Snapshot(4, 0);


			if (Thread32First(hSnapshot, ref te) && Thread32Next(hSnapshot, out te))
			{
				do
				{
					if (te.th32OwnerProcessID == proc.Id)
					{
						OpenThreadHandles.Add(OpenThread(0x1FFFFF, false, te.th32ThreadID));
					}
				}
				while (Thread32Next(hSnapshot, out te));
			}
		}


		private static void SetDebugRegisters(HookRegister register, uint hookLocation, ref ThreadContext ct, bool remove)
		{
			if (remove)
			{
				uint flagBit = 0;
				switch (register)
				{
					case HookRegister.DR0:
						flagBit = 1 << 0;
						ct.Dr0 = 0;
						break;
					case HookRegister.DR1:
						flagBit = 1 << 2;
						ct.Dr1 = 0;
						break;
					case HookRegister.DR2:
						flagBit = 1 << 4;
						ct.Dr2 = 0;
						break;
					case HookRegister.DR3:
						flagBit = 1 << 6;
						ct.Dr3 = 0;
						break;
				}
				ct.Dr7 &= ~flagBit;
			}
			else
			{
				switch (register)
				{
					case HookRegister.DR0:
						ct.Dr0 = (uint)hookLocation;
						ct.Dr7 |= 1 << 0;
						break;
					case HookRegister.DR1:
						ct.Dr1 = (uint)hookLocation;
						ct.Dr7 |= 1 << 2;
						break;
					case HookRegister.DR2:
						ct.Dr2 = (uint)hookLocation;
						ct.Dr7 |= 1 << 4;
						break;
					case HookRegister.DR3:
						ct.Dr3 = (uint)hookLocation;
						ct.Dr7 |= 1 << 6;
						break;
				}
				ct.Dr6 = 0;
			}
		}


		private static void SetThreadHook(HookItem item, bool remove)
		{
			var ctx = new ThreadContext();
			ctx.ContextFlags = 65559;
			foreach (uint openThreadHandle in OpenThreadHandles)
			{
				SuspendThread(openThreadHandle);
				GetThreadContext(openThreadHandle, ref ctx);


				SetDebugRegisters(item.Register, item.Location, ref ctx, remove);
				item.Hooked = !remove;


				SetThreadContext(openThreadHandle, ref ctx);
				ResumeThread(openThreadHandle);
			}
		}


		public static void InstallHardwareHook(Process proc)
		{
			// Open the proc with full privs so we can attach the debugger later.
			OpenProcess(0x1FFFFFu, false, (uint)proc.Id);
			// Open all the threads for use with installing/removing the DR hooks
			OpenAllThreads(proc);


			// Ideally should never be hit, but we just need to ensure we check it anyway.
			if (!_debugActiveAlready && !DebugActiveProcess((uint)proc.Id))
				throw new Exception("Failed to attach debugger!");


			_debugActiveAlready = true;


			DebugSetProcessKillOnExit(0);


			try
			{
				while (!_removeHook)
				{
					// Useless double-checking of hook states. Sanity really...
					if (Hooks.Any(i => !i.Hooked))
					{
						foreach (HookItem hookItem in Hooks)
						{
							if (!hookItem.Hooked)
							{
								SetThreadHook(hookItem, false);
							}
						}
					}


					// And begin waiting for debug events...
					var ctx = new ThreadContext();
					ctx.ContextFlags = 0x10017;
					DEBUG_EVENT evt;
					if (!WaitForDebugEvent(out evt, 0xFFFFFFFF))
					{
						continue;
					}


					if (evt.Exception.ExceptionRecord.ExceptionCode != 0x80000004) // EXCEPTION_SINGLE_STEP
					{
						ContinueDebugEvent((uint)evt.dwProcessId, (uint)evt.dwThreadId, 0x80010001); // EXCEPTION_CONTINUE
					}
					else
					{
						// Re-open the thread so we can get the context info we need.
						uint hThread = OpenThread(0x1FFFFFu, false, (uint)evt.dwThreadId);


						GetThreadContext(hThread, ref ctx);


						ctx.EFlags |= 0x10040; // CONTEXT_FULL


						// NOTE: The callback "call" part is in a catch-all exception handler.
						// This is to prevent people from crashing the application with the DR hook installed.
						// This won't stop people from breaking the context though!
						// Feel free to modify this to ensure people see the exception if need be.
						try
						{
							// Call our callback!
							// Find it by the location we Hooked it to (eip is the current address FYI)
							HookItem hook = Hooks.FirstOrDefault(h => (uint)h.Location == ctx.Eip);
							if (hook != null)
								hook.Callback(ref ctx);
						}
						catch
						{
						}


						// Set the new thread context (if it was changed)
						SetThreadContext(hThread, ref ctx);


						// And we're done with the thread.
						CloseHandle(hThread);


						// Move along...
						ContinueDebugEvent((uint)evt.dwProcessId, (uint)evt.dwThreadId, 0x10002u);
					}
				}
			}
			finally
			{
				// Thread is closing, or something else is making this function "leave"
				// Make sure we drop all the DR hooks to make sure we don't start spewing exceptions at the client.
				foreach (HookItem hookItem in Hooks)
				{
					if (hookItem.Hooked)
					{
						SetThreadHook(hookItem, true);
					}
				}
			}
		}


		~ExternalProcessHook()
		{
			// Finalizer Dispose() is to ensure this gets run every single time, regardless of Dispose() being called.
			// Application closing doesn't *always* run Dispose. Hence this!
			Dispose();
		}



		


		private class HookItem
		{
			public HandleHookCallback Callback;
			public bool Hooked;
			public uint Location;
			public HookRegister Register;
		}
	}*/

}
