namespace Input
{
    public sealed class InputProvider : RothnagInputActionAsset
    {
        private static InputProvider _instance;
        public static InputProvider instance => _instance ??= new();

        private InputProvider()
        {
            Enable();
        }
    }
}