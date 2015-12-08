using System.Windows.Forms;

namespace OFClassLibrary.Frameworks.WinForms
{
    public static class InputHelper
    {

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static bool IsCtrlPressed() => IsKeyPressed(Keys.Control);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static bool IsAltPressed() => IsKeyPressed(Keys.Alt);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static bool IsShiftPressed() => IsKeyPressed(Keys.Shift);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static bool IsMouseLeftPressed() => IsMouseButtonPressed(MouseButtons.Left);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static bool IsMouseRightPressed() => IsMouseButtonPressed(MouseButtons.Right);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static bool IsMouseMiddlePressed() => IsMouseButtonPressed(MouseButtons.Middle);


        private static bool IsKeyPressed(Keys key) => (Control.ModifierKeys & key) == key;

        private static bool IsMouseButtonPressed(MouseButtons button) => (Control.MouseButtons & button) == button;

    }
}
