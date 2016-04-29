using SharpDX.Windows;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SharpDX.Direct2D1;

using Factory2D = SharpDX.Direct2D1.Factory;
using FactoryDW = SharpDX.DirectWrite.Factory;
using AlphaMode = SharpDX.Direct2D1.AlphaMode;
using SharpDX.DXGI;

namespace Modpit {
    public class SimpleWindow {
        private readonly TimeHandler clock = new TimeHandler();
        private FormWindowState currentFormWindowState;
        private bool disposed;
        private Form form;
        private float frameAccumulator;
        private int frameCount;

        public Factory2D Factory2D { get; private set; }
        public FactoryDW FactoryDW { get; private set; }
        public WindowRenderTarget RenderTarget2D { get; private set; }
        public SolidColorBrush SceneColorBrush { get; private set; }
        
        ~SimpleWindow() {
            if (!disposed) {
                Dispose(false);
                disposed = false;
            }
        }
        public void Dispose() {
            if (!disposed) {
                Dispose(true);
                disposed = true;
            }
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposeManagedResources) {
            if (disposeManagedResources) {
                if (form != null) {
                    form.Dispose();
                }
            }
        }

        protected IntPtr DisplayHandle {
            get {
                return form.Handle;
            }
        }
        public float FrameDelta { get; private set; }
        public float FPS { get; private set; }
        
        protected virtual Form CreateForm() {
            return new RenderForm("Title") {
                ClientSize = new Size(1280, 720)
            };
        }
        private void OnUpdate() {
            FrameDelta = (float)clock.Update();
            Update(clock);
        }

        public void Run() {
            form = CreateForm();
            Factory2D = new Factory2D();
            FactoryDW = new FactoryDW();

            HwndRenderTargetProperties props = new HwndRenderTargetProperties();
            props.Hwnd = DisplayHandle;
            props.PixelSize = new SharpDX.Size2(1280, 720);
            props.PresentOptions = PresentOptions.Immediately;

            RenderTarget2D = new WindowRenderTarget(Factory2D, new RenderTargetProperties(new PixelFormat(Format.Unknown, AlphaMode.Premultiplied)), props);
            RenderTarget2D.AntialiasMode = AntialiasMode.PerPrimitive;

            SceneColorBrush = new SolidColorBrush(RenderTarget2D, new SharpDX.Mathematics.Interop.RawColor4(1f, 1f, 1f, 1f));
            Initialize();

            bool isFormClosed = false, formIsResizing = false;

            form.MouseClick += HandleMouseClick;
            form.MouseMove += HandleMouseMove;
            form.KeyDown += HandleKeyDown;
            form.KeyUp += HandleKeyUp;
            form.Resize += (o, args) => {
                if (form.WindowState != currentFormWindowState) {
                    HandleResize(o, args);
                }
                currentFormWindowState = form.WindowState;
            };
            form.ResizeBegin += (o, args) => {
                formIsResizing = true;
            };
            form.ResizeEnd += (o, args) => {
                formIsResizing = false;
                HandleResize(o, args);
            };
            form.FormClosed += (o, args) => {
                isFormClosed = true;
            };

            LoadContent();

            clock.Start();
            BeginRun();
            RenderLoop.Run(form, () => {
                if (isFormClosed) return;
                OnUpdate();
                if (!formIsResizing) Render();
            });

            UnloadContent();
            EndRun();
            Dispose();
        }
        public void Exit() {
            form.Close();
        }
        private void Render() {
            frameAccumulator += FrameDelta;
            ++frameCount;
            if (frameAccumulator >= 1.0f) {
                FPS = frameCount / frameAccumulator;

                form.Text = FPS.ToString();
                frameAccumulator = 0.0f;
                frameCount = 0;
            }

            RenderTarget2D.BeginDraw();
            BeginDraw();
            Draw(clock);
            EndDraw();
            RenderTarget2D.EndDraw();
        }

        protected virtual void Initialize() {

        }
        protected virtual void LoadContent() {

        }
        protected virtual void UnloadContent() {

        }
        protected virtual void Update(TimeHandler time) {

        }
        protected virtual void BeginRun() {

        }
        protected virtual void EndRun() {

        }
        protected virtual void BeginDraw() {

        }
        protected virtual void Draw(TimeHandler time) {

        }
        protected virtual void EndDraw() {

        }

        protected virtual void MouseClick(MouseEventArgs e) {

        }
        protected virtual void KeyDown(KeyEventArgs e) {

        }
        protected virtual void KeyUp(KeyEventArgs e) {

        }
        protected virtual void MouseMove(MouseEventArgs e) {

        }

        private void HandleMouseClick(object sender, MouseEventArgs e) {
            MouseClick(e);
        }
        private void HandleKeyDown(object sender, KeyEventArgs e) {
            KeyDown(e);
        }
        private void HandleKeyUp(object sender, KeyEventArgs e) {
            KeyUp(e);
        }
        private void HandleMouseMove(object sender, MouseEventArgs e) {
            MouseMove(e);
        }
        private void HandleResize(object sender, EventArgs e) {
            if (form.WindowState == FormWindowState.Minimized) return;
        }
    }
}
