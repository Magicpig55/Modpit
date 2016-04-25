using System;
using System.Windows.Forms;

using SharpDX.Windows;
using SharpDX.Direct3D;
using SharpDX.Direct3D11;
using SharpDX.DXGI;
using SharpDX.Direct2D1;
using SharpDX.Mathematics.Interop;

using Device = SharpDX.Direct3D11.Device; // Lmao explicit declaration
using Resource = SharpDX.Direct3D11.Resource;
using Factory2D = SharpDX.Direct2D1.Factory;
using FactoryDW = SharpDX.DirectWrite.Factory;
using FactoryDX = SharpDX.DXGI.Factory;
using FeatureLevelDX = SharpDX.Direct3D.FeatureLevel;
using AlphaMode = SharpDX.Direct2D1.AlphaMode;

namespace Modpit {
    public class Window : IDisposable {
        private readonly TimeHandler clock = new TimeHandler();
        private FormWindowState currentFormWindowState;
        private bool disposed;
        private Form form;
        private float frameAccumulator;
        private int frameCount;

        Device device;
        SwapChain swapChain;
        Texture2D backBuffer;
        RenderTargetView backBufferView;

        public Factory2D Factory2D { get; private set; }
        public FactoryDW FactoryDirectWrite { get; private set; }
        public RenderTarget RenderTarget2D { get; private set; }
        public SolidColorBrush SceneColorBrush { get; private set; }

        ~Window() {
            if (!disposed) {
                Dispose();
                disposed = true;
            }
        }

        public void Dispose() {
            if(!disposed) {
                Dispose(true);
                disposed = true;
            }
            GC.SuppressFinalize(this);
        }
        public virtual void Dispose(bool disposeManagedResources) {
            if (disposeManagedResources) {
                if (form != null) form.Dispose();
            }
        }

        protected IntPtr DisplayHandle {
            get {
                return form.Handle;
            }
        }
        public Device Device {
            get {
                return device;
            }
        }
        public Texture2D BackBuffer {
            get {
                return backBuffer;
            }
        }
        public RenderTargetView BackBufferView {
            get {
                return backBufferView;
            }
        }

        public float FrameDelta { get; private set; }
        public float FPS { get; private set; }

        public virtual Form CreateForm() {
            return new RenderForm("Test") {
                ClientSize = new System.Drawing.Size(1280, 720)
            };
        }

        private void OnUpdate() {
            FrameDelta = (float)clock.Update();
            Update(clock);
        }
        private void Render() {
            frameAccumulator += FrameDelta;
            ++frameCount;
            if (frameAccumulator >= 1.0f) {
                FPS = frameCount / frameAccumulator;
                frameAccumulator = 0;
                frameCount = 0;
            }
            form.Text = "Test " + FPS.ToString();
            Device.ImmediateContext.Rasterizer.SetViewport(0, 0, 1280, 720);
            Device.ImmediateContext.OutputMerger.SetTargets(backBufferView);
            RenderTarget2D.BeginDraw();
            BeginDraw();
            Draw(clock);
            RenderTarget2D.EndDraw();
            EndDraw();
            swapChain.Present(0, PresentFlags.None);
        }
        public void Exit() {
            form.Close();
        }

        public void Run() {
            form = CreateForm();

            SwapChainDescription desc = new SwapChainDescription() {
                BufferCount = 1,
                ModeDescription = new ModeDescription(1280, 720, new Rational(60, 1), Format.R8G8B8A8_UNorm),
                IsWindowed = true,
                OutputHandle = DisplayHandle,
                SampleDescription = new SampleDescription(1, 0),
                SwapEffect = SwapEffect.Discard,
                Usage = Usage.RenderTargetOutput
            };
            Device.CreateWithSwapChain(DriverType.Hardware, DeviceCreationFlags.BgraSupport, new[] { FeatureLevelDX.Level_10_0 }, desc, out device, out swapChain);
            FactoryDX factory = swapChain.GetParent<FactoryDX>();
            factory.MakeWindowAssociation(DisplayHandle, WindowAssociationFlags.IgnoreAll);
            backBuffer = Resource.FromSwapChain<Texture2D>(swapChain, 0);
            backBufferView = new RenderTargetView(device, backBuffer);

            Factory2D = new Factory2D();
            using (Surface surface = backBuffer.QueryInterface<Surface>()) {
                RenderTarget2D = new RenderTarget(Factory2D, surface, new RenderTargetProperties(new PixelFormat(Format.Unknown, AlphaMode.Premultiplied)));
            }
            RenderTarget2D.AntialiasMode = AntialiasMode.PerPrimitive;
            FactoryDirectWrite = new FactoryDW();
            SceneColorBrush = new SolidColorBrush(RenderTarget2D, new RawColor4(1, 1, 1, 1));

            Initialize();
            bool isFormClosed = false;
            bool formIsResizing = false;

            form.MouseClick += HandleMouseClick;
            form.KeyDown += HandleKeyDown;
            form.KeyUp += HandleKeyUp;
            form.FormClosed += (o, args) => { isFormClosed = true; };
            form.ResizeBegin += (o, args) => { formIsResizing = true; };
            form.ResizeEnd += (o, args) => {
                formIsResizing = false;
                HandleResize(o, args);
            };
            form.Resize += (o, args) => {
                if (form.WindowState != currentFormWindowState) {
                    HandleResize(o, args);
                }
                currentFormWindowState = form.WindowState;
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
        protected virtual void Initialize() {

        }
        protected virtual void LoadContent() {

        }
        protected virtual void UnloadContent() {

        }
        protected virtual void Update(TimeHandler time) {

        }
        protected virtual void BeginDraw() {

        }
        protected virtual void Draw(TimeHandler time) {

        }
        protected virtual void EndDraw() {

        }
        protected virtual void BeginRun() {

        }
        protected virtual void EndRun() {

        }

        public void MouseClick(MouseEventArgs e) {

        }
        public void KeyDown(KeyEventArgs e) {

        }
        public void KeyUp(KeyEventArgs e) {

        }

        private void HandleMouseClick (object sender, MouseEventArgs e) {
            MouseClick(e);
        }
        private void HandleKeyDown(object sender, KeyEventArgs e) {
            KeyDown(e);
        }
        private void HandleKeyUp(object sender, KeyEventArgs e) {
            KeyUp(e);
        }
        private void HandleResize(object sender, EventArgs e) {
            if (form.WindowState == FormWindowState.Minimized) return;
        }
    }
}
