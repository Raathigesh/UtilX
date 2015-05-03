using Microsoft.Win32;

namespace UtilX
{
    interface IStartUpRegister
    {
        void RegisterAsStartUp(bool isStartAtWindowsStartUp);
    }

    class StartUpRegister : IStartUpRegister
    {
        private readonly RegistryKey _registryKey;
        private const string AppName = "UtilX";

        public StartUpRegister()
        {
            _registryKey = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
        }
        public void RegisterAsStartUp(bool isStartAtWindowsStartUp)
        {
            if (isStartAtWindowsStartUp)
            {
                var path = System.Reflection.Assembly.GetExecutingAssembly().Location;

                if (_registryKey.GetValue(AppName) != path)
                {
                    _registryKey.SetValue(AppName, path);    
                }
            }
            else
            {
                _registryKey.DeleteValue(AppName, false);
            }
            
            
        }
    }
}
