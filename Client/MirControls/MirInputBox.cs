﻿using System;
using System.Drawing;
using System.Windows.Forms;
using Client.MirGraphics;

namespace Client.MirControls
{
    public sealed class MirInputBox : MirImageControl
    {
        public readonly MirLabel CaptionLabel;
        public readonly MirButton OKButton, CancelButton;
        public readonly MirTextBox InputTextBox;


        public MirInputBox(string message)
        {
            Modal = true;
            Movable = false;

            Index = 360;//660;
            Library = Libraries.Prguse;

            Location = new Point((Settings.ScreenWidth - Size.Width) / 2, (Settings.ScreenHeight - Size.Height) / 2);

            CaptionLabel = new MirLabel
            {
                DrawFormat = TextFormatFlags.WordBreak,
                Location = new Point(25, 25),
                Size = new Size(235, 40),
                Parent = this,
                Text = message,
            };

            InputTextBox = new MirTextBox
            {
                Parent = this,
                Border = true,
                BorderColour = Color.Lime,
                Location = new Point(23, 86),
                Size = new Size(240, 19),
                MaxLength = 50,
            };
            InputTextBox.SetFocus();
            InputTextBox.TextBox.KeyPress += MirInputBox_KeyPress;

            OKButton = new MirButton
            {
                HoverIndex = 361,//201,
                Index = 361,//200,
                Library = Libraries.Prguse,
                Location = new Point(360, 135),//new Point(360, 157),
                Parent = this,
                PressedIndex = 362,//202,
            };

            CancelButton = new MirButton
            {
                HoverIndex = 365,//204,
                Index = 365,//203,
                Library = Libraries.Prguse,
                Location = new Point(360, 135),//new Point(360, 157),
                Parent = this,
                PressedIndex = 366,//205,
            };
            CancelButton.Click += DisposeDialog;
        }

        void MirInputBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                if (OKButton != null && !OKButton.IsDisposed)
                    OKButton.InvokeMouseClick(EventArgs.Empty);
                e.Handled = true;
            }
            else if (e.KeyChar == (char)Keys.Escape)
            {
                if (CancelButton != null && !CancelButton.IsDisposed)
                    CancelButton.InvokeMouseClick(EventArgs.Empty);
                e.Handled = true;
            }
        }
        void DisposeDialog(object sender, EventArgs e)
        {
            Dispose();
        }

        public override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            e.Handled = true;
        }
        public override void OnKeyUp(KeyEventArgs e)
        {
            base.OnKeyUp(e);
            e.Handled = true;
        }
        public override void OnKeyPress(KeyPressEventArgs e)
        {
            base.OnKeyPress(e);

            if (e.KeyChar == (char)Keys.Escape)
            {
                if (CancelButton != null && !CancelButton.IsDisposed)
                    CancelButton.InvokeMouseClick(EventArgs.Empty);
            }
            else if (e.KeyChar == (char)Keys.Enter)
            {
                if (OKButton != null && !OKButton.IsDisposed)
                    OKButton.InvokeMouseClick(EventArgs.Empty);

            }
            e.Handled = true;
        }

        public void Show()
        {
            if (Parent != null) return;

            Parent = MirScene.ActiveScene;

            Highlight();

            for (int i = 0; i < Program.Form.Controls.Count; i++)
            {
                TextBox T = Program.Form.Controls[i] as TextBox;
                if (T != null && T.Tag != null && T.Tag != null)
                    ((MirTextBox)T.Tag).DialogChanged();
            }
        }


        #region Disposable

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            if (!disposing) return;

            for (int i = 0; i < Program.Form.Controls.Count; i++)
            {
                TextBox T = (TextBox)Program.Form.Controls[i];
                if (T != null && T.Tag != null && T.Tag != null)
                    ((MirTextBox)T.Tag).DialogChanged();
            }
        }

        #endregion

    }
}
