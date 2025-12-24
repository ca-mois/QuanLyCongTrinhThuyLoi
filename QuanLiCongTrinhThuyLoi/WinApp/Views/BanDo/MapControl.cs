using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;

namespace WinApp.Views.BanDo
{
    public class MapControl : UserControl
    {
        private GMapControl gMapControl;
        private GMapOverlay markersOverlay;
        private Panel panelTop;
        private Label lblTitle;
        private ComboBox cboMapType;

        public MapControl()
        {
            InitializeComponent();
            InitializeMap();
        }

        private void InitializeComponent()
        {
            this.panelTop = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.cboMapType = new System.Windows.Forms.ComboBox();
            this.gMapControl = new GMap.NET.WindowsForms.GMapControl();
            this.panelTop.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelTop
            // 
            this.panelTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            this.panelTop.Controls.Add(this.lblTitle);
            this.panelTop.Controls.Add(this.cboMapType);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(1200, 64);
            this.panelTop.TabIndex = 1;
            // 
            // lblTitle
            // 
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(10, 10);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(753, 68);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "BẢN ĐỒ PHÂN BỐ CÔNG TRÌNH THỦY LỢI";
            // 
            // cboMapType
            // 
            this.cboMapType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboMapType.Items.AddRange(new object[] {
            "Google Map",
            "Google Satellite",
            "Google Hybrid",
            "OpenStreetMap"});
            this.cboMapType.Location = new System.Drawing.Point(10, 50);
            this.cboMapType.Name = "cboMapType";
            this.cboMapType.Size = new System.Drawing.Size(200, 28);
            this.cboMapType.TabIndex = 1;
            this.cboMapType.SelectedIndexChanged += new System.EventHandler(this.cboMapType_SelectedIndexChanged_1);
            // 
            // gMapControl
            // 
            this.gMapControl.Bearing = 0F;
            this.gMapControl.CanDragMap = true;
            this.gMapControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gMapControl.EmptyTileColor = System.Drawing.Color.Navy;
            this.gMapControl.GrayScaleMode = false;
            this.gMapControl.HelperLineOption = GMap.NET.WindowsForms.HelperLineOptions.DontShow;
            this.gMapControl.LevelsKeepInMemmory = 5;
            this.gMapControl.Location = new System.Drawing.Point(0, 64);
            this.gMapControl.MarkersEnabled = true;
            this.gMapControl.MaxZoom = 18;
            this.gMapControl.MinZoom = 2;
            this.gMapControl.MouseWheelZoomEnabled = true;
            this.gMapControl.MouseWheelZoomType = GMap.NET.MouseWheelZoomType.MousePositionAndCenter;
            this.gMapControl.Name = "gMapControl";
            this.gMapControl.NegativeMode = false;
            this.gMapControl.PolygonsEnabled = true;
            this.gMapControl.RetryLoadTile = 0;
            this.gMapControl.RoutesEnabled = true;
            this.gMapControl.ScaleMode = GMap.NET.WindowsForms.ScaleModes.Integer;
            this.gMapControl.SelectedAreaFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(65)))), ((int)(((byte)(105)))), ((int)(((byte)(225)))));
            this.gMapControl.ShowTileGridLines = false;
            this.gMapControl.Size = new System.Drawing.Size(1200, 736);
            this.gMapControl.TabIndex = 0;
            this.gMapControl.Zoom = 6D;
            // 
            // MapControl
            // 
            this.Controls.Add(this.gMapControl);
            this.Controls.Add(this.panelTop);
            this.Name = "MapControl";
            this.Size = new System.Drawing.Size(1200, 800);
            this.panelTop.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        private void InitializeMap()
        {
            try
            {
                // Cấu hình GMap.NET
                GMaps.Instance.Mode = AccessMode.ServerAndCache;
                gMapControl.MapProvider = GMapProviders.GoogleMap;

                // Vị trí trung tâm Việt Nam
                gMapControl.Position = new PointLatLng(16.0544, 108.2022); // Đà Nẵng
                gMapControl.MinZoom = 5;
                gMapControl.MaxZoom = 18;
                gMapControl.Zoom = 6;

                // Cài đặt điều khiển
                gMapControl.ShowCenter = false;
                gMapControl.DragButton = MouseButtons.Left;

                // Tạo overlay cho markers
                markersOverlay = new GMapOverlay("markers");
                gMapControl.Overlays.Add(markersOverlay);

                System.Diagnostics.Debug.WriteLine("Map initialized successfully");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"InitializeMap Error: {ex.Message}");
                MessageBox.Show($"Lỗi khởi tạo bản đồ: {ex.Message}\n\nVui lòng kiểm tra kết nối internet!",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CboMapType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                switch (cboMapType.SelectedIndex)
                {
                    case 0:
                        gMapControl.MapProvider = GMapProviders.GoogleMap;
                        break;
                    case 1:
                        gMapControl.MapProvider = GMapProviders.GoogleSatelliteMap;
                        break;
                    case 2:
                        gMapControl.MapProvider = GMapProviders.GoogleHybridMap;
                        break;
                    case 3:
                        gMapControl.MapProvider = GMapProviders.OpenStreetMap;
                        break;
                }
                gMapControl.ReloadMap();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi chuyển loại bản đồ: {ex.Message}", "Lỗi");
            }
        }

        public void LoadDuLieu(List<Models.CongTrinh> data)
        {
            try
            {
                if (data == null || data.Count == 0)
                {
                    MessageBox.Show("Không có dữ liệu công trình!", "Thông báo");
                    return;
                }

                // Xóa markers cũ
                markersOverlay.Markers.Clear();

                int count = 0;
                foreach (var ct in data)
                {
                    var coords = ParseCoordinates(ct.DuLieuGIS);
                    if (coords.HasValue)
                    {
                        // Tạo marker
                        GMarkerGoogle marker = new GMarkerGoogle(
                            coords.Value,
                            GMarkerGoogleType.red_big_stop
                        );

                        // Tooltip
                        marker.ToolTipText = $"{ct.TenCongTrinh}\n" +
                                           $"Mã: {ct.MaHieu}\n" +
                                           $"{ct.DiaDiem}";
                        marker.ToolTipMode = MarkerTooltipMode.OnMouseOver;
                        marker.Tag = ct;

                        markersOverlay.Markers.Add(marker);
                        count++;
                    }
                }

                // Zoom để hiển thị tất cả markers
                if (markersOverlay.Markers.Count > 0)
                {
                    gMapControl.ZoomAndCenterMarkers("markers");
                }

                lblTitle.Text = $"BẢN ĐỒ - Hiển thị {count}/{data.Count} công trình";

                System.Diagnostics.Debug.WriteLine($"Loaded {count} markers on map");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi load dữ liệu: {ex.Message}", "Lỗi");
            }
        }

        private PointLatLng? ParseCoordinates(string gisData)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(gisData))
                    return null;

                // Format: "lat,lng" hoặc "21.0285,105.8542"
                var parts = gisData.Split(',');
                if (parts.Length >= 2)
                {
                    string latStr = parts[0].Trim().Replace("lat:", "").Replace("latitude:", "");
                    string lngStr = parts[1].Trim().Replace("lng:", "").Replace("lon:", "");

                    if (double.TryParse(latStr, out double lat) &&
                        double.TryParse(lngStr, out double lng))
                    {
                        return new PointLatLng(lat, lng);
                    }
                }
            }
            catch { }
            return null;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                gMapControl?.Dispose();
            }
            base.Dispose(disposing);
        }

        private void cboMapType_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }
    }
}