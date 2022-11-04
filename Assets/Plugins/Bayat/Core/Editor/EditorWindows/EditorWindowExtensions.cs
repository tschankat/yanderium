using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEditor;

namespace Bayat.Core.EditorWindows
{

    public static class EditorWindowExtensions
    {

        public static void Center(this EditorWindow window)
        {
            var mainEditorWindowPosition = EditorWindowUtility.MainEditorWindowPosition;

            window.position = new Rect
            (
                mainEditorWindowPosition.position + mainEditorWindowPosition.size / 2 - window.position.size / 2,
                window.position.size
            );
        }

        public static bool IsFocused(this EditorWindow window)
        {
            return EditorWindow.focusedWindow == window;
        }

    }

}