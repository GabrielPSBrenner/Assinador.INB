//using System;
//using System.Runtime.InteropServices;
//using System.Security.Principal;
//using System.Security.Permissions;
//using System.Windows.Forms;

//namespace INB.Assinador.View
//{
//    public static class Impersonate
//    {


//        [DllImport("advapi32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
//        public static extern bool LogonUser(String lpszUsername, String lpszDomain, String lpszPassword,
//            int dwLogonType, int dwLogonProvider, ref IntPtr phToken);

//        //[DllImport("kernel32.dll", CharSet = System.Runtime.InteropServices.CharSet.Auto)]
//        //private unsafe static extern int FormatMessage(int dwFlags, ref IntPtr lpSource,
//        //    int dwMessageId, int dwLanguageId, ref String lpBuffer, int nSize, IntPtr* Arguments);

//        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
//        public extern static bool CloseHandle(IntPtr handle);

//        [DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
//        public extern static bool DuplicateToken(IntPtr ExistingTokenHandle,
//            int SECURITY_IMPERSONATION_LEVEL, ref IntPtr DuplicateTokenHandle);


//    }
//}

