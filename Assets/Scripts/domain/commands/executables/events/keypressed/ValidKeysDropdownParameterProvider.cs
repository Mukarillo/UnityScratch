using System;
using System.Linq;
using domain.parameter;
using UnityEngine;

namespace domain.commands.executables.events.keypressed
{
    public enum ValidKeys
    {
        Any,
        Space,
        DownArrow,
        LeftArrow,
        RightArrow,
        UpArrow,
        A,
        B,
        C,
        D,
        E,
        F,
        G,
        H,
        I,
        J,
        K,
        L,
        M,
        N,
        O,
        P,
        Q,
        R,
        S,
        T,
        U,
        V,
        X,
        Y,
        Z,
        Alpha0,
        Alpha1,
        Alpha2,
        Alpha3,
        Alpha4,
        Alpha5,
        Alpha6,
        Alpha7,
        Alpha8,
        Alpha9,
        Escape
    }
    
    public static class ValidKeysExtensions
    {
        public static KeyCode ToKeyCode (this ValidKeys key)
        {
            switch (key)
            {
                case ValidKeys.Space: return KeyCode.Space;
                case ValidKeys.DownArrow: return KeyCode.DownArrow;
                case ValidKeys.LeftArrow: return KeyCode.LeftArrow;
                case ValidKeys.RightArrow: return KeyCode.RightArrow;
                case ValidKeys.UpArrow: return KeyCode.UpArrow;
                case ValidKeys.A: return KeyCode.A;
                case ValidKeys.B: return KeyCode.B;
                case ValidKeys.C: return KeyCode.C;
                case ValidKeys.D: return KeyCode.D;
                case ValidKeys.E: return KeyCode.E;
                case ValidKeys.F: return KeyCode.F;
                case ValidKeys.G: return KeyCode.G;
                case ValidKeys.H: return KeyCode.H;
                case ValidKeys.I: return KeyCode.I;
                case ValidKeys.J: return KeyCode.J;
                case ValidKeys.K: return KeyCode.K;
                case ValidKeys.L: return KeyCode.L;
                case ValidKeys.M: return KeyCode.M;
                case ValidKeys.N: return KeyCode.N;
                case ValidKeys.O: return KeyCode.O;
                case ValidKeys.P: return KeyCode.P;
                case ValidKeys.Q: return KeyCode.Q;
                case ValidKeys.R: return KeyCode.R;
                case ValidKeys.S: return KeyCode.S;
                case ValidKeys.T: return KeyCode.T;
                case ValidKeys.U: return KeyCode.U;
                case ValidKeys.V: return KeyCode.V;
                case ValidKeys.X: return KeyCode.X;
                case ValidKeys.Y: return KeyCode.Y;
                case ValidKeys.Z: return KeyCode.Z;
                case ValidKeys.Alpha0: return KeyCode.Alpha0;
                case ValidKeys.Alpha1: return KeyCode.Alpha1;
                case ValidKeys.Alpha2: return KeyCode.Alpha2;
                case ValidKeys.Alpha3: return KeyCode.Alpha3;
                case ValidKeys.Alpha4: return KeyCode.Alpha4;
                case ValidKeys.Alpha5: return KeyCode.Alpha5;
                case ValidKeys.Alpha6: return KeyCode.Alpha6;
                case ValidKeys.Alpha7: return KeyCode.Alpha7;
                case ValidKeys.Alpha8: return KeyCode.Alpha8;
                case ValidKeys.Alpha9: return KeyCode.Alpha9;
                case ValidKeys.Escape: return KeyCode.Escape;
            }

            throw new ArgumentOutOfRangeException($"Invalid conversion of ValidKey {key}");
        }
    }
    
    public class ValidKeysDropdownParameterProvider : DropdownParameterProvider<ValidKeys>
    {
        public ValidKeys[] GetDropdownElements()
        {
            return Enum.GetValues(typeof(ValidKeys)).Cast<ValidKeys>().ToArray();
        }
    }
}