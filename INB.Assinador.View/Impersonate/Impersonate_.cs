//using System;
//using System.Runtime.InteropServices;
//using System.Security.Principal;
//using System.Threading;


//public class Impersonate_ : IDisposable
//{

//    /* TODO ERROR: Skipped RegionDirectiveTrivia */
//    public enum eLogonType : int
//    {
//        LOGON32_LOGON_INTERACTIVE = 2,
//        LOGON32_LOGON_NETWORK = 3,
//        LOGON32_LOGON_NETWORK_CLEARTEXT = 8,
//        LOGON32_LOGON_BATCH = 4,
//        LOGON32_LOGON_SERVICE = 5,
//        LOGON32_LOGON_UNLOCK = 7,
//        LOGON32_LOGON_NEW_CREDENTIALS = 9
//    }

//    public enum eLogonProvider : int
//    {
//        LOGON32_PROVIDER_DEFAULT = 0,
//        LOGON32_PROVIDER_WINNT35 = 1,
//        LOGON32_PROVIDER_WINNT40 = 2,
//        LOGON32_PROVIDER_WINNT50 = 3
//    }

//    public enum eImpersonationLevel : int
//    {
//        SecurityAnonymous = 0,
//        SecurityIdentification = 1,
//        SecurityImpersonation = 2,
//        SecurityDelegation = 3
//    }

//    /* TODO ERROR: Skipped EndRegionDirectiveTrivia */
//    private WindowsImpersonationContext impersonationContext;

//    [DllImport("advapi32.dll")]
//    static extern int LogonUserA(string lpszUsername, string lpszDomain, string lpszPassword, int dwLogonType, int dwLogonProvider, ref IntPtr phToken);





//    // Declare Function LogonUser Lib "advapi32.dll" (ByVal lpszUsername As String, _
//    // ByVal lpszDomain As String, ByVal lpszPassword As String, _
//    // ByVal dwLogonType As Integer, ByVal dwLogonProvider As Integer, _
//    // ByRef phToken As IntPtr) As Integer

//    [DllImport("advapi32.dll", CharSet = CharSet.Auto)]
//    static extern int DuplicateToken(IntPtr ExistingTokenHandle, int ImpersonationLevel, ref IntPtr DuplicateTokenHandle);


//    [DllImport("advapi32.dll", CharSet = CharSet.Auto)]
//    static extern long RevertToSelf();
//    [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
//    static extern long CloseHandle(IntPtr handle);

//    public bool impersonateValidUser(string userName, string domain, string password)
//    {
//        bool impersonateValidUserRet = false;
//        WindowsIdentity tempWindowsIdentity;
//        var token = IntPtr.Zero;
//        var tokenDuplicate = IntPtr.Zero;
//        impersonateValidUserRet = false;
//        if (Convert.ToBoolean(RevertToSelf()))
//        {
//            if (Impersonate_.LogonUserA(userName, domain, password, (int)eLogonType.LOGON32_LOGON_NETWORK, (int)eLogonProvider.LOGON32_PROVIDER_DEFAULT, ref token) != 0)
//            {
//                if (DuplicateToken(token, (int)eImpersonationLevel.SecurityImpersonation, ref tokenDuplicate) != 0)
//                {
//                    tempWindowsIdentity = new WindowsIdentity(tokenDuplicate);
//                    impersonationContext = tempWindowsIdentity.Impersonate();
//                    if (impersonationContext is object)
//                    {
//                        impersonateValidUserRet = true;
//                    }
//                }
//            }
//        }

//        if (!tokenDuplicate.Equals(IntPtr.Zero))
//        {
//            CloseHandle(tokenDuplicate);
//        }

//        if (!token.Equals(IntPtr.Zero))
//        {
//            CloseHandle(token);
//        }

//        return impersonateValidUserRet;
//    }

//    public void undoImpersonation()
//    {
//        impersonationContext.Undo();
//    }

//    public WindowsImpersonationContext ImpersonateValidUserAndSetThreadPrincipal(string userName, string domain, string password)
//    {
//        WindowsImpersonationContext impersonationContext = null;
//        WindowsIdentity tempWindowsIdentity;
//        var token = IntPtr.Zero;
//        var tokenDuplicate = IntPtr.Zero;
//        IIdentity user;
//        WindowsPrincipal principal;
//        try
//        {
//            if (Convert.ToBoolean(RevertToSelf()))
//            {
//                if (Impersonate_.LogonUserA(userName, domain, password, (int)eLogonType.LOGON32_LOGON_INTERACTIVE, (int)eLogonProvider.LOGON32_PROVIDER_DEFAULT, ref token) != 0)
//                {
//                    if (DuplicateToken(token, 2, ref tokenDuplicate) != 0)
//                    {
//                        tempWindowsIdentity = new WindowsIdentity(tokenDuplicate);
//                        impersonationContext = tempWindowsIdentity.Impersonate();
//                        // apply impersonation to threading
//                        user = new WindowsIdentity(token, "NTLM", WindowsAccountType.Normal, true);
//                        principal = new WindowsPrincipal((WindowsIdentity)user);
//                        Thread.CurrentPrincipal = principal;
//                    }
//                }
//            }
//            return impersonationContext;
//        }
//        catch (Exception ex)
//        {
//            throw ex;
//        }
//        finally
//        {
//            if (!tokenDuplicate.Equals(IntPtr.Zero))
//            {
//                CloseHandle(tokenDuplicate);
//            }

//            if (!token.Equals(IntPtr.Zero))
//            {
//                CloseHandle(token);
//            }
//            //return impersonationContext;
//        }
        
//    }

//    public void undoImpersonateThreadPrincipal()
//    {
//        try
//        {
//            WindowsPrincipal principal;
//            principal = new WindowsPrincipal(WindowsIdentity.GetCurrent());
//            Thread.CurrentPrincipal = principal;
//        }
//        catch (Exception ex)
//        {
//            throw ex;
//        }
//    }

//    public void Dispose()
//    {
//       // undoImpersonation();
//    }
//}
