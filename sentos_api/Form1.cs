using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using sentos_api.Models;
using sentos_api.Services;

namespace sentos_api
{
    public partial class Form1 : DevExpress.XtraEditors.XtraForm
    {
        private SentosApiService _sentosService;
        private SentosApiConfig _sentosConfig;

        public Form1()
        {
            InitializeComponent();
            SetupOrderGridView();
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            await InitializeServicesAsync();
        }

        private async Task InitializeServicesAsync()
        {
            // SentOS API konfigürasyonu (Sentos panelinden alınan gerçek bilgiler)
            _sentosConfig = new SentosApiConfig
            {
                BaseUrl = "https://cappafe.sentos.com.tr/api",
                ApiKey = "2bc45b71-f1e7-4749-b81a-4398d910829a",
                SecretKey = "J1w5784rXRZhqBsn9lw8OvubhfflzvnKSDaavzay",
                TimeoutSeconds = 30,
                Username = "ocelikli@editbilisim.com.tr",
                Password = "Edit2025*",
                CompanyName = "cappafe"
            };

            // Servisleri başlat
            _sentosService = new SentosApiService(_sentosConfig);
            
            // Otomatik giriş yap
            try
            {
                var loginSuccess = await _sentosService.EnsureAuthenticatedAsync();
                if (loginSuccess)
                {
                    MessageBox.Show("Sentos'a başarıyla bağlandı!\n\nAPI Bilgileri:\nURL: https://cappafe.sentos.com.tr/api\nAPI Key: 2bc45b71-f1e7-4749-b81a-4398d910829a\n\nKimlik doğrulama aktif.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Sentos'a bağlanılamadı!\n\nOlası nedenler:\n- API Key/Secret yanlış\n- API endpoint'i bulunamadı\n- Ağ bağlantısı problemi\n\nLütfen kontrol edin:\nURL: https://cappafe.sentos.com.tr/api\nAPI Key: 2bc45b71-f1e7-4749-b81a-4398d910829a\nAPI Secret: J1w5784rXRZhqBsn9lw8OvubhfflzvnKSDaavzay", "Bağlantı Hatası", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Bağlantı hatası: {ex.Message}\n\nDetay: {ex.InnerException?.Message}\n\nDebug: Visual Studio Output penceresini kontrol edin.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
            // Test için örnek ürünleri ekle (isteğe bağlı)
           AddSampleProducts(); // Gerektiğinde açın
            
            // Test için örnek siparişleri ekle (isteğe bağlı)
            AddSampleOrders();
        }

        private void SetupOrderGridView()
        {
            // Master-Detail yapısını kur
            gridViewOrders.MasterRowGetChildList += GridViewOrders_MasterRowGetChildList;
            gridViewOrders.MasterRowGetRelationCount += GridViewOrders_MasterRowGetRelationCount;
            gridViewOrders.MasterRowGetRelationName += GridViewOrders_MasterRowGetRelationName;
            gridViewOrders.MasterRowEmpty += GridViewOrders_MasterRowEmpty;
            
            // Detail view oluştur
            CreateDetailView();
            
            // Master-Detail ayarları
            gridViewOrders.OptionsDetail.EnableMasterViewMode = false;
            gridViewOrders.OptionsDetail.ShowDetailTabs = false;
            gridViewOrders.OptionsDetail.AutoZoomDetail = false;
            gridViewOrders.OptionsDetail.AllowExpandEmptyDetails = true;
            gridViewOrders.OptionsDetail.AllowOnlyOneMasterRowExpanded = false;
            
            // Products grid için BandedGridView kurulumu
            SetupProductsBandedGridView();
        }

        private void CreateDetailView()
        {
            // gridViewOrderLines için sütunları ekle
            var colLineId = new DevExpress.XtraGrid.Columns.GridColumn();
            colLineId.Caption = "ID";
            colLineId.FieldName = "id";
            colLineId.Name = "colLineId";
            colLineId.Visible = true;
            colLineId.VisibleIndex = 0;
            colLineId.Width = 60;

            var colLineSKU = new DevExpress.XtraGrid.Columns.GridColumn();
            colLineSKU.Caption = "SKU";
            colLineSKU.FieldName = "sku";
            colLineSKU.Name = "colLineSKU";
            colLineSKU.Visible = true;
            colLineSKU.VisibleIndex = 1;
            colLineSKU.Width = 120;

            var colLineName = new DevExpress.XtraGrid.Columns.GridColumn();
            colLineName.Caption = "Ürün Adı";
            colLineName.FieldName = "name";
            colLineName.Name = "colLineName";
            colLineName.Visible = true;
            colLineName.VisibleIndex = 2;
            colLineName.Width = 300;

            var colLineQuantity = new DevExpress.XtraGrid.Columns.GridColumn();
            colLineQuantity.Caption = "Miktar";
            colLineQuantity.FieldName = "quantity";
            colLineQuantity.Name = "colLineQuantity";
            colLineQuantity.Visible = true;
            colLineQuantity.VisibleIndex = 3;
            colLineQuantity.Width = 80;

            var colLinePrice = new DevExpress.XtraGrid.Columns.GridColumn();
            colLinePrice.Caption = "Fiyat";
            colLinePrice.FieldName = "price";
            colLinePrice.Name = "colLinePrice";
            colLinePrice.Visible = true;
            colLinePrice.VisibleIndex = 4;
            colLinePrice.Width = 100;

            var colLineAmount = new DevExpress.XtraGrid.Columns.GridColumn();
            colLineAmount.Caption = "Tutar";
            colLineAmount.FieldName = "amount";
            colLineAmount.Name = "colLineAmount";
            colLineAmount.Visible = true;
            colLineAmount.VisibleIndex = 5;
            colLineAmount.Width = 100;

            var colLineCurrency = new DevExpress.XtraGrid.Columns.GridColumn();
            colLineCurrency.Caption = "Para Birimi";
            colLineCurrency.FieldName = "currency";
            colLineCurrency.Name = "colLineCurrency";
            colLineCurrency.Visible = true;
            colLineCurrency.VisibleIndex = 6;
            colLineCurrency.Width = 80;

            var colLineVatRate = new DevExpress.XtraGrid.Columns.GridColumn();
            colLineVatRate.Caption = "KDV Oranı";
            colLineVatRate.FieldName = "vat_rate";
            colLineVatRate.Name = "colLineVatRate";
            colLineVatRate.Visible = true;
            colLineVatRate.VisibleIndex = 7;
            colLineVatRate.Width = 80;

            var colLineColor = new DevExpress.XtraGrid.Columns.GridColumn();
            colLineColor.Caption = "Renk";
            colLineColor.FieldName = "color";
            colLineColor.Name = "colLineColor";
            colLineColor.Visible = true;
            colLineColor.VisibleIndex = 8;
            colLineColor.Width = 100;

            var colLineModel = new DevExpress.XtraGrid.Columns.GridColumn();
            colLineModel.Caption = "Model";
            colLineModel.FieldName = "model_value";
            colLineModel.Name = "colLineModel";
            colLineModel.Visible = true;
            colLineModel.VisibleIndex = 9;
            colLineModel.Width = 120;

            // Sütunları gridViewOrderLines'a ekle
            gridViewOrderLines.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
                colLineId, colLineSKU, colLineName, colLineQuantity, colLinePrice, 
                colLineAmount, colLineCurrency, colLineVatRate, colLineColor, colLineModel
            });

            // Master-Detail relation kur - sizin eklediğiniz gridViewOrderLines ile
            var relation = new DevExpress.XtraGrid.GridLevelNode();
            relation.LevelTemplate = gridViewOrderLines; // Sizin eklediğiniz view
            relation.RelationName = "OrderLines";
            gridControlOrders.LevelTree.Nodes.Add(relation);
        }

        private void GridViewOrders_MasterRowGetRelationCount(object sender, DevExpress.XtraGrid.Views.Grid.MasterRowGetRelationCountEventArgs e)
        {
            e.RelationCount = 1; // Sadece lines için bir relation
        }

        private void GridViewOrders_MasterRowGetRelationName(object sender, DevExpress.XtraGrid.Views.Grid.MasterRowGetRelationNameEventArgs e)
        {
            e.RelationName = "OrderLines";
        }

        private void GridViewOrders_MasterRowGetChildList(object sender, DevExpress.XtraGrid.Views.Grid.MasterRowGetChildListEventArgs e)
        {
            var order = gridViewOrders.GetRow(e.RowHandle) as Order;
            if (order != null && order.lines != null && order.lines.Count > 0)
            {
                e.ChildList = order.lines;
            }
        }

        private void GridViewOrders_MasterRowEmpty(object sender, DevExpress.XtraGrid.Views.Grid.MasterRowEmptyEventArgs e)
        {
            var order = gridViewOrders.GetRow(e.RowHandle) as Order;
            e.IsEmpty = order == null || order.lines == null || order.lines.Count == 0;
        }

        private void SetupProductsBandedGridView()
        {
            // gridViewProducts'ı BandedGridView'e çevir
            var bandedGridView = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridView();
            bandedGridView.Name = "bandedGridViewProducts";
            
            // Ana band'ları oluştur
            var basicInfoBand = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            basicInfoBand.Caption = "Temel Bilgiler";
            basicInfoBand.Name = "basicInfoBand";
            basicInfoBand.VisibleIndex = 0;
            basicInfoBand.Width = 300;

            var priceInfoBand = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            priceInfoBand.Caption = "Fiyat Bilgileri";
            priceInfoBand.Name = "priceInfoBand";
            priceInfoBand.VisibleIndex = 1;
            priceInfoBand.Width = 200;

            var stockInfoBand = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            stockInfoBand.Caption = "Stok Bilgileri";
            stockInfoBand.Name = "stockInfoBand";
            stockInfoBand.VisibleIndex = 2;
            stockInfoBand.Width = 150;

            var platformPricesBand = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            platformPricesBand.Caption = "Platform Fiyatları";
            platformPricesBand.Name = "platformPricesBand";
            platformPricesBand.VisibleIndex = 3;
            platformPricesBand.Width = 400;

            var variantInfoBand = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            variantInfoBand.Caption = "Varyant Bilgileri";
            variantInfoBand.Name = "variantInfoBand";
            variantInfoBand.VisibleIndex = 4;
            variantInfoBand.Width = 200;

            // Temel Bilgiler Band'ına sütunlar ekle
            var colId = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            colId.Caption = "ID";
            colId.FieldName = "id";
            colId.Name = "colId";
            colId.OwnerBand = basicInfoBand;
            colId.Visible = true;
            colId.VisibleIndex = 0;
            colId.Width = 60;

            var colSKU = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            colSKU.Caption = "SKU";
            colSKU.FieldName = "sku";
            colSKU.Name = "colSKU";
            colSKU.OwnerBand = basicInfoBand;
            colSKU.Visible = true;
            colSKU.VisibleIndex = 1;
            colSKU.Width = 120;

            var colName = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            colName.Caption = "Ürün Adı";
            colName.FieldName = "name";
            colName.Name = "colName";
            colName.OwnerBand = basicInfoBand;
            colName.Visible = true;
            colName.VisibleIndex = 2;
            colName.Width = 200;

            var colInvoiceName = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            colInvoiceName.Caption = "Fatura Adı";
            colInvoiceName.FieldName = "invoice_name";
            colInvoiceName.Name = "colInvoiceName";
            colInvoiceName.OwnerBand = basicInfoBand;
            colInvoiceName.Visible = true;
            colInvoiceName.VisibleIndex = 3;
            colInvoiceName.Width = 180;

            var colBrand = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            colBrand.Caption = "Marka";
            colBrand.FieldName = "brand";
            colBrand.Name = "colBrand";
            colBrand.OwnerBand = basicInfoBand;
            colBrand.Visible = true;
            colBrand.VisibleIndex = 4;
            colBrand.Width = 100;

            var colDescription = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            colDescription.Caption = "Açıklama";
            colDescription.FieldName = "description";
            colDescription.Name = "colDescription";
            colDescription.OwnerBand = basicInfoBand;
            colDescription.Visible = true;
            colDescription.VisibleIndex = 5;
            colDescription.Width = 150;

            var colBarcode = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            colBarcode.Caption = "Barkod";
            colBarcode.FieldName = "barcode";
            colBarcode.Name = "colBarcode";
            colBarcode.OwnerBand = basicInfoBand;
            colBarcode.Visible = true;
            colBarcode.VisibleIndex = 6;
            colBarcode.Width = 120;

            // BandedGridView'e band'ları ekle
            bandedGridView.Bands.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] {
                basicInfoBand, priceInfoBand, stockInfoBand, platformPricesBand, variantInfoBand
            });

            // Fiyat Bilgileri Band'ına sütunlar ekle
            var colPurchasePrice = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            colPurchasePrice.Caption = "Alış Fiyatı";
            colPurchasePrice.FieldName = "purchase_price";
            colPurchasePrice.Name = "colPurchasePrice";
            colPurchasePrice.OwnerBand = priceInfoBand;
            colPurchasePrice.Visible = true;
            colPurchasePrice.VisibleIndex = 0;
            colPurchasePrice.Width = 100;
            colPurchasePrice.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            colPurchasePrice.DisplayFormat.FormatString = "N2";

            var colSalePrice = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            colSalePrice.Caption = "Satış Fiyatı";
            colSalePrice.FieldName = "sale_price";
            colSalePrice.Name = "colSalePrice";
            colSalePrice.OwnerBand = priceInfoBand;
            colSalePrice.Visible = true;
            colSalePrice.VisibleIndex = 1;
            colSalePrice.Width = 100;
            colSalePrice.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            colSalePrice.DisplayFormat.FormatString = "N2";

            var colCurrency = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            colCurrency.Caption = "Para Birimi";
            colCurrency.FieldName = "currency";
            colCurrency.Name = "colCurrency";
            colCurrency.OwnerBand = priceInfoBand;
            colCurrency.Visible = true;
            colCurrency.VisibleIndex = 2;
            colCurrency.Width = 80;

            var colVatRate = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            colVatRate.Caption = "KDV Oranı";
            colVatRate.FieldName = "vat_rate";
            colVatRate.Name = "colVatRate";
            colVatRate.OwnerBand = priceInfoBand;
            colVatRate.Visible = true;
            colVatRate.VisibleIndex = 3;
            colVatRate.Width = 80;

            // Stok Bilgileri Band'ına sütunlar ekle
            var colVolumetricWeight = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            colVolumetricWeight.Caption = "Hacimsel Ağırlık";
            colVolumetricWeight.FieldName = "volumetric_weight";
            colVolumetricWeight.Name = "colVolumetricWeight";
            colVolumetricWeight.OwnerBand = stockInfoBand;
            colVolumetricWeight.Visible = true;
            colVolumetricWeight.VisibleIndex = 0;
            colVolumetricWeight.Width = 120;

            var colCategoryId = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            colCategoryId.Caption = "Kategori ID";
            colCategoryId.FieldName = "category_id";
            colCategoryId.Name = "colCategoryId";
            colCategoryId.OwnerBand = stockInfoBand;
            colCategoryId.Visible = true;
            colCategoryId.VisibleIndex = 1;
            colCategoryId.Width = 100;

            // Platform Fiyatları Band'ına sütunlar ekle (ana platformlar)
            var colB2CListPrice = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            colB2CListPrice.Caption = "B2C Liste";
            colB2CListPrice.FieldName = "prices.b2c.list_price";
            colB2CListPrice.Name = "colB2CListPrice";
            colB2CListPrice.OwnerBand = platformPricesBand;
            colB2CListPrice.Visible = true;
            colB2CListPrice.VisibleIndex = 0;
            colB2CListPrice.Width = 80;

            var colB2CSalePrice = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            colB2CSalePrice.Caption = "B2C Satış";
            colB2CSalePrice.FieldName = "prices.b2c.sale_price";
            colB2CSalePrice.Name = "colB2CSalePrice";
            colB2CSalePrice.OwnerBand = platformPricesBand;
            colB2CSalePrice.Visible = true;
            colB2CSalePrice.VisibleIndex = 1;
            colB2CSalePrice.Width = 80;

            var colN11ListPrice = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            colN11ListPrice.Caption = "N11 Liste";
            colN11ListPrice.FieldName = "prices.n11.list_price";
            colN11ListPrice.Name = "colN11ListPrice";
            colN11ListPrice.OwnerBand = platformPricesBand;
            colN11ListPrice.Visible = true;
            colN11ListPrice.VisibleIndex = 2;
            colN11ListPrice.Width = 80;

            var colN11SalePrice = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            colN11SalePrice.Caption = "N11 Satış";
            colN11SalePrice.FieldName = "prices.n11.sale_price";
            colN11SalePrice.Name = "colN11SalePrice";
            colN11SalePrice.OwnerBand = platformPricesBand;
            colN11SalePrice.Visible = true;
            colN11SalePrice.VisibleIndex = 3;
            colN11SalePrice.Width = 80;

            var colTrendyolListPrice = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            colTrendyolListPrice.Caption = "Trendyol Liste";
            colTrendyolListPrice.FieldName = "prices.trendyol.list_price";
            colTrendyolListPrice.Name = "colTrendyolListPrice";
            colTrendyolListPrice.OwnerBand = platformPricesBand;
            colTrendyolListPrice.Visible = true;
            colTrendyolListPrice.VisibleIndex = 4;
            colTrendyolListPrice.Width = 100;

            var colTrendyolSalePrice = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            colTrendyolSalePrice.Caption = "Trendyol Satış";
            colTrendyolSalePrice.FieldName = "prices.trendyol.sale_price";
            colTrendyolSalePrice.Name = "colTrendyolSalePrice";
            colTrendyolSalePrice.OwnerBand = platformPricesBand;
            colTrendyolSalePrice.Visible = true;
            colTrendyolSalePrice.VisibleIndex = 5;
            colTrendyolSalePrice.Width = 100;

            // Varyant Bilgileri Band'ına sütunlar ekle
            var colVariantCount = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            colVariantCount.Caption = "Varyant Sayısı";
            colVariantCount.FieldName = "variants.Count";
            colVariantCount.Name = "colVariantCount";
            colVariantCount.OwnerBand = variantInfoBand;
            colVariantCount.Visible = true;
            colVariantCount.VisibleIndex = 0;
            colVariantCount.Width = 120;

            var colTransferStatus = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            colTransferStatus.Caption = "Aktarım Durumu";
            colTransferStatus.FieldName = "TransferStatus";
            colTransferStatus.Name = "colTransferStatus";
            colTransferStatus.OwnerBand = variantInfoBand;
            colTransferStatus.Visible = true;
            colTransferStatus.VisibleIndex = 1;
            colTransferStatus.Width = 120;

            // BandedGridView'e sütunları ekle
            bandedGridView.Columns.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn[] {
                colId, colSKU, colName, colInvoiceName, colBrand, colDescription, colBarcode,
                colPurchasePrice, colSalePrice, colCurrency, colVatRate,
                colVolumetricWeight, colCategoryId,
                colB2CListPrice, colB2CSalePrice, colN11ListPrice, colN11SalePrice, 
                colTrendyolListPrice, colTrendyolSalePrice,
                colVariantCount, colTransferStatus
            });

            // gridControlProducts'ın view'ını değiştir
            gridControlProducts.MainView = bandedGridView;
            
            // BandedGridView ayarları
            bandedGridView.OptionsView.ShowBands = true;
            bandedGridView.OptionsView.ShowGroupPanel = false;
            bandedGridView.OptionsView.ShowIndicator = true;
            bandedGridView.OptionsView.ShowAutoFilterRow = true;
            bandedGridView.OptionsSelection.MultiSelect = true;
            bandedGridView.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
        }





        #region Product Transfer Events

        private async void btnRefreshProducts_Click(object sender, EventArgs e)
        {
            await RefreshProductsAsync();
        }

        private async Task RefreshProductsAsync()
        {
            try
            {
                btnRefreshProducts.Enabled = false;
                btnRefreshProducts.Text = "Yükleniyor...";

                // Grid'deki mevcut verileri yenile (SentOS'tan ürünleri getir)
                var result = await _sentosService.GetProductsAsync(1, 100, "active");

                if (result.Success)
                {
                    gridControlProducts.DataSource = result.Data;
                    XtraMessageBox.Show($"{result.Data.Count} ürün listelendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    XtraMessageBox.Show($"Hata: {result.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Beklenmeyen hata: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnRefreshProducts.Enabled = true;
                btnRefreshProducts.Text = "Ürünleri Listele";
            }
        }

        private async void btnTransferProducts_Click(object sender, EventArgs e)
        {
            try
            {
                var selectedRows = gridViewProducts.GetSelectedRows();
                if (selectedRows.Length == 0)
                {
                    XtraMessageBox.Show("Lütfen aktarılacak ürünleri seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var result = XtraMessageBox.Show($"{selectedRows.Length} ürün SentOS'a aktarılacak. Devam etmek istiyor musunuz?",
                    "Onay", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result != DialogResult.Yes)
                    return;

                btnTransferProducts.Enabled = false;
                btnTransferProducts.Text = "Aktarılıyor...";

                var productsToTransfer = new List<Product>();
                foreach (int rowHandle in selectedRows)
                {
                    var product = gridViewProducts.GetRow(rowHandle) as Product;
                    if (product != null)
                    {
                        productsToTransfer.Add(product);
                    }
                }

                var transferResults = await TransferProductsToSentosAsync(productsToTransfer);

                int successCount = transferResults.Count(r => r.Success);
                int failCount = transferResults.Count(r => !r.Success);

                string message = $"Aktarım tamamlandı.\nBaşarılı: {successCount}\nBaşarısız: {failCount}";

                if (failCount > 0)
                {
                    var failedProducts = transferResults.Where(r => !r.Success).Select(r => r.Message).ToList();
                    message += $"\n\nHatalar:\n{string.Join("\n", failedProducts)}";
                }

                XtraMessageBox.Show(message, "Aktarım Sonucu", MessageBoxButtons.OK,
                    failCount > 0 ? MessageBoxIcon.Warning : MessageBoxIcon.Information);

                // Listeyi yenile
                await RefreshProductsAsync();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Beklenmeyen hata: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnTransferProducts.Enabled = true;
                btnTransferProducts.Text = "Seçili Ürünleri Aktar";
            }
        }

        #endregion

        #region Order Transfer Events

        private async void btnRefreshOrders_Click(object sender, EventArgs e)
        {
            await RefreshOrdersAsync();
        }

        private async Task RefreshOrdersAsync()
        {
            try
            {
                btnRefreshOrders.Enabled = false;
                btnRefreshOrders.Text = "Yükleniyor...";

                var result = await _sentosService.GetOrdersAsync(1, 100, 1); // 1: Onay Bekliyor

                if (result.Success)
                {
                    gridControlOrders.DataSource = result.Data;
                    XtraMessageBox.Show($"{result.Data.Count} sipariş listelendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    XtraMessageBox.Show($"Hata: {result.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Beklenmeyen hata: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnRefreshOrders.Enabled = true;
                btnRefreshOrders.Text = "Siparişleri Listele";
            }
        }

        private async void btnTransferOrders_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    var selectedRows = gridViewOrders.GetSelectedRows();
            //    if (selectedRows.Length == 0)
            //    {
            //        XtraMessageBox.Show("Lütfen aktarılacak siparişleri seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //        return;
            //    }

            //    var result = XtraMessageBox.Show($"{selectedRows.Length} sipariş ERPiS'e aktarılacak. Devam etmek istiyor musunuz?",
            //        "Onay", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            //    if (result != DialogResult.Yes)
            //        return;

            //    btnTransferOrders.Enabled = false;
            //    btnTransferOrders.Text = "Aktarılıyor...";

            //    var ordersToTransfer = new List<Order>();
            //    foreach (int rowHandle in selectedRows)
            //    {
            //        var order = gridViewOrders.GetRow(rowHandle) as Order;
            //        if (order != null)
            //        {
            //            ordersToTransfer.Add(order);
            //        }
            //    }

            //    int successCount = 0;
            //    int failCount = 0;
            //    var errorMessages = new List<string>();

            //    // oc_insert_siparis_sentos prosedürünü çağır
            //    var procedureResult = await CallInsertSiparisSentosProcedureAsync(ordersToTransfer);

            //    if (procedureResult.Success)
            //    {
            //        successCount = ordersToTransfer.Count;
            //        failCount = 0;
            //    }
            //    else
            //    {
            //        successCount = 0;
            //        failCount = ordersToTransfer.Count;
            //        errorMessages.Add(procedureResult.Message);
            //        if (procedureResult.Errors != null)
            //        {
            //            errorMessages.AddRange(procedureResult.Errors);
            //        }
            //    }

            //    string message = $"Aktarım tamamlandı.\nBaşarılı: {successCount}\nBaşarısız: {failCount}";

            //    if (failCount > 0)
            //    {
            //        message += $"\n\nHatalar:\n{string.Join("\n", errorMessages)}";
            //    }

            //    XtraMessageBox.Show(message, "Aktarım Sonucu", MessageBoxButtons.OK,
            //        failCount > 0 ? MessageBoxIcon.Warning : MessageBoxIcon.Information);

            //    // Listeyi yenile
            //    await RefreshOrdersAsync();
            //}
            //catch (Exception ex)
            //{
            //    XtraMessageBox.Show($"Beklenmeyen hata: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
            //finally
            //{
            //    btnTransferOrders.Enabled = true;
            //    btnTransferOrders.Text = "Seçili Siparişleri Aktar";
            //}
        }

        #endregion

        #region SentOS Operations

        private async Task<List<ProductResponse>> TransferProductsToSentosAsync(List<Product> products)
        {
            var results = new List<ProductResponse>();

            foreach (var product in products)
            {
                try
                {
                    ProductResponse result;
                    
                    // Önce ürünün SentOS'ta var olup olmadığını kontrol et
                    var existingProduct = await _sentosService.GetProductByIdAsync(product.Id);
                    
                    if (existingProduct.Success && existingProduct.Data != null)
                    {
                        // Ürün varsa güncelle (PUT /products/{id})
                        result = await _sentosService.UpdateProductAsync(product.Id, product);
                        result.Message = $"Ürün '{product.Name}' güncellendi";
                    }
                    else
                    {
                        // Ürün yoksa oluştur (POST /products)
                        result = await _sentosService.CreateProductAsync(product);
                        result.Message = $"Ürün '{product.Name}' oluşturuldu";
                    }
                    
                    results.Add(result);

                    // Başarılıysa grid'deki durumu güncelle
                    if (result.Success)
                    {
                        product.TransferStatus = "Aktarıldı";
                        product.UpdatedDate = DateTime.Now;
                    }
                    else
                    {
                        product.TransferStatus = "Hata";
                    }
                }
                catch (Exception ex)
                {
                    results.Add(new ProductResponse
                    {
                        Success = false,
                        Message = $"Ürün '{product.Name}' aktarımında hata: {ex.Message}",
                        Errors = new List<string> { ex.Message }
                    });
                    
                    product.TransferStatus = "Hata";
                }
            }

            return results;
        }

        private async Task UpdateProductTransferStatusInErpisAsync(int productId, string status)
        {
            // Bu metod artık kullanılmıyor, grid'deki durumu güncelliyoruz
        }

        // JSON dosyasından örnek ürünleri yükle
        private void AddSampleProducts()
        {
            try
            {
                // JSON dosyasını oku
                var jsonPath = Path.Combine(Application.StartupPath, "sample_products.json");
                
                if (!File.Exists(jsonPath))
                {
                    // JSON dosyası yoksa varsayılan örnekleri kullan
                    AddDefaultSampleProducts();
                    return;
                }

                var jsonContent = File.ReadAllText(jsonPath);
                var sampleProducts = LoadProductsFromJson(jsonContent);
                
                gridControlProducts.DataSource = sampleProducts;
                
                // Debug için bilgi göster
                System.Diagnostics.Debug.WriteLine($"JSON'dan {sampleProducts.Count} ürün yüklendi.");
            }
            catch (Exception ex)
            {
                // Hata durumunda varsayılan örnekleri kullan
                System.Diagnostics.Debug.WriteLine($"JSON yükleme hatası: {ex.Message}");
                AddDefaultSampleProducts();
            }
        }

        /// <summary>
        /// JSON içeriğini Product nesnelerine dönüştürür
        /// </summary>
        private List<Product> LoadProductsFromJson(string jsonContent)
        {
            var products = new List<Product>();
            
            try
            {
                // JSON'u dinamik nesneye parse et
                var jsonProducts = Newtonsoft.Json.JsonConvert.DeserializeObject<List<dynamic>>(jsonContent);
                
                foreach (var jsonProduct in jsonProducts)
                {
                    var product = new Product
                    {
                        SKU = jsonProduct.sku?.ToString(),
                        CategoryId = Convert.ToInt32(jsonProduct.category_id ?? 0),
                        Name = jsonProduct.name?.ToString(),
                        InvoiceName = jsonProduct.invoice_name?.ToString(),
                        Brand = jsonProduct.brand?.ToString(),
                        Description = jsonProduct.description?.ToString(),
                        Currency = jsonProduct.currency?.ToString() ?? "TL",
                        PurchasePrice = Convert.ToDecimal(jsonProduct.purchase_price ?? 0),
                        SalePrice = Convert.ToDecimal(jsonProduct.sale_price ?? 0),
                        VatRate = Convert.ToInt32(jsonProduct.vat_rate ?? 0),
                        VolumetricWeight = Convert.ToDecimal(jsonProduct.volumetric_weight ?? 0),
                        Barcode = jsonProduct.barcode?.ToString(),
                        DescriptionDetail = jsonProduct.description_detail?.ToString(),
                    CreatedDate = DateTime.Now.AddDays(-30),
                    UpdatedDate = DateTime.Now,
                    TransferStatus = "Beklemede"
                    };

                    // Stok bilgilerini ekle
                    if (jsonProduct.stocks != null)
                    {
                        product.Stocks = new List<ProductStock>();
                        foreach (var stock in jsonProduct.stocks)
                        {
                            product.Stocks.Add(new ProductStock
                            {
                                Warehouse = Convert.ToInt32(stock.warehouse ?? 1),
                                Stock = Convert.ToInt32(stock.stock ?? 0)
                            });
                        }
                    }

                    // Resim bilgilerini ekle
                    if (jsonProduct.images != null)
                    {
                        product.Images = new List<ProductImage>();
                        foreach (var image in jsonProduct.images)
                        {
                            product.Images.Add(new ProductImage
                            {
                                Id = Convert.ToInt32(image.id ?? 0),
                                Url = image.url?.ToString()
                            });
                        }
                    }

                    // Platform fiyatlarını ekle
                    if (jsonProduct.prices != null)
                    {
                        product.Prices = new ProductPrices();
                        
                        // B2C fiyatları
                        if (jsonProduct.prices.b2c != null)
                        {
                            product.Prices.B2C = new PlatformPrice
                            {
                                ListPrice = jsonProduct.prices.b2c.list_price?.ToString(),
                                SalePrice = jsonProduct.prices.b2c.sale_price?.ToString()
                            };
                        }

                        // N11 fiyatları
                        if (jsonProduct.prices.n11 != null)
                        {
                            product.Prices.N11 = new PlatformPrice
                            {
                                ListPrice = jsonProduct.prices.n11.list_price?.ToString(),
                                SalePrice = jsonProduct.prices.n11.sale_price?.ToString()
                            };
                        }

                        // Trendyol fiyatları
                        if (jsonProduct.prices.trendyol != null)
                        {
                            product.Prices.Trendyol = new PlatformPrice
                            {
                                ListPrice = jsonProduct.prices.trendyol.list_price?.ToString(),
                                SalePrice = jsonProduct.prices.trendyol.sale_price?.ToString()
                            };
                        }

                        // Diğer platformlar için de benzer şekilde eklenebilir...
                    }

                    // Varyantları ekle
                    if (jsonProduct.variants != null)
                    {
                        product.Variants = new List<ProductVariant>();
                        foreach (var variant in jsonProduct.variants)
                        {
                            var productVariant = new ProductVariant
                            {
                                SKU = variant.sku?.ToString(),
                                Barcode = variant.barcode?.ToString(),
                                Color = variant.color?.ToString(),
                                Model = new ProductModel
                                {
                                    Name = variant.model?.name?.ToString(),
                                    Value = variant.model?.value?.ToString()
                                },
                                Stocks = new List<ProductStock>(),
                                Images = new List<ProductImage>()
                            };

                            // Varyant stokları
                            if (variant.stocks != null)
                            {
                                foreach (var variantStock in variant.stocks)
                                {
                                    productVariant.Stocks.Add(new ProductStock
                                    {
                                        Warehouse = Convert.ToInt32(variantStock.warehouse ?? 1),
                                        Stock = Convert.ToInt32(variantStock.stock ?? 0)
                                    });
                                }
                            }

                            product.Variants.Add(productVariant);
                        }
                    }

                    products.Add(product);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"JSON parse hatası: {ex.Message}");
                throw;
            }

            return products;
        }

        /// <summary>
        /// Varsayılan örnek ürünleri ekler (JSON dosyası yoksa)
        /// </summary>
        private void AddDefaultSampleProducts()
        {
            var sampleProducts = new List<Product>
            {
                new Product
                {
                    SKU = "TEST-001",
                    Name = "Test Ürünü 1",
                    InvoiceName = "Test Ürünü 1",
                    Brand = "Test Marka",
                    Description = "Test açıklaması",
                    Currency = "TL",
                    PurchasePrice = 100.00m,
                    SalePrice = 150.00m,
                    VatRate = 18,
                    VolumetricWeight = 1.0m,
                    Barcode = "1234567890123",
                    DescriptionDetail = "Detaylı test açıklaması",
                    Stocks = new List<ProductStock>
                    {
                        new ProductStock { Warehouse = 1, Stock = 10 }
                    },
                    Images = new List<ProductImage>(),
                    Prices = new ProductPrices(),
                    Variants = new List<ProductVariant>(),
                    CreatedDate = DateTime.Now.AddDays(-30),
                    UpdatedDate = DateTime.Now,
                    TransferStatus = "Beklemede"
                },
                new Product
                {
                    SKU = "TEST-002",
                    Name = "Test Ürünü 2",
                    InvoiceName = "Test Ürünü 2",
                    Brand = "Test Marka",
                    Description = "Test açıklaması",
                    Currency = "TL",
                    PurchasePrice = 200.00m,
                    SalePrice = 300.00m,
                    VatRate = 18,
                    VolumetricWeight = 1.5m,
                    Barcode = "1234567890124",
                    DescriptionDetail = "Detaylı test açıklaması",
                    Stocks = new List<ProductStock>
                    {
                        new ProductStock { Warehouse = 1, Stock = 5 }
                    },
                    Images = new List<ProductImage>(),
                    Prices = new ProductPrices(),
                    Variants = new List<ProductVariant>(),
                    CreatedDate = DateTime.Now.AddDays(-15),
                    UpdatedDate = DateTime.Now,
                    TransferStatus = "Beklemede"
                }
            };

            gridControlProducts.DataSource = sampleProducts;
        }

        // Test için örnek sipariş ekleme metodu
        private void AddSampleOrders()
        {
            // DataSet oluştur
            var dataSet = new System.Data.DataSet();
            
            // Orders tablosu
            var ordersTable = new System.Data.DataTable("Orders");
            ordersTable.Columns.Add("id", typeof(int));
            ordersTable.Columns.Add("order_id", typeof(string));
            ordersTable.Columns.Add("order_code", typeof(string));
            ordersTable.Columns.Add("status", typeof(int));
            ordersTable.Columns.Add("source", typeof(string));
            ordersTable.Columns.Add("shop", typeof(string));
            ordersTable.Columns.Add("order_date", typeof(DateTime));
            ordersTable.Columns.Add("total", typeof(string));
            ordersTable.Columns.Add("shipping_total", typeof(string));
            ordersTable.Columns.Add("cargo_provider", typeof(string));
            ordersTable.Columns.Add("cargo_number", typeof(string));
            ordersTable.Columns.Add("has_invoice", typeof(string));
            ordersTable.Columns.Add("invoice_type", typeof(string));
            ordersTable.Columns.Add("invoice_number", typeof(string));
            ordersTable.Columns.Add("invoice_url", typeof(string));
            ordersTable.Columns.Add("note", typeof(string));
            ordersTable.Columns.Add("customer_name", typeof(string));
            ordersTable.Columns.Add("customer_phone", typeof(string));
            ordersTable.Columns.Add("customer_email", typeof(string));
            ordersTable.Columns.Add("invoice_address", typeof(string));
            ordersTable.Columns.Add("shipment_address", typeof(string));
            
            // OrderLines tablosu
            var linesTable = new System.Data.DataTable("OrderLines");
            linesTable.Columns.Add("order_id", typeof(int));
            linesTable.Columns.Add("id", typeof(int));
            linesTable.Columns.Add("sku", typeof(string));
            linesTable.Columns.Add("name", typeof(string));
            linesTable.Columns.Add("quantity", typeof(int));
            linesTable.Columns.Add("price", typeof(string));
            linesTable.Columns.Add("amount", typeof(string));
            linesTable.Columns.Add("currency", typeof(string));
            linesTable.Columns.Add("vat_rate", typeof(int));
            linesTable.Columns.Add("color", typeof(string));
            linesTable.Columns.Add("model_name", typeof(string));
            linesTable.Columns.Add("model_value", typeof(string));
            
            // Veri ekle
            ordersTable.Rows.Add(1, "181849999", "203129724888", 5, "N11", "Test Mağaza", 
                DateTime.Parse("2019-01-28T13:48:00.000Z"), "29.9", "0", "Aras Kargo", 
                "968001002817777", "yes", "EARSIV", "INV001", 
                "https://test.sentos.com.tr/siparis_faturalari/181849999_fatura.pdf", 
                "Test siparişi", "Leland Calwell", "0(556) 111 11 11", "email@gmail.com",
                "Bosna Hersek Mh. Ali Fuat Cebesoy Bulvarı no 99 daire:48 kat:12.",
                "Bosna Hersek Mh. Ali Fuat Cebesoy Bulvarı no 99 daire:48 kat:12.");
                
            ordersTable.Rows.Add(2, "181850000", "203129724889", 3, "Trendyol", "Moda Mağaza", 
                DateTime.Parse("2019-01-29T10:30:00.000Z"), "150.0", "15.0", "Yurtiçi Kargo", 
                "968001002817778", "yes", "EARSIV", "INV002", 
                "https://test.sentos.com.tr/siparis_faturalari/181850000_fatura.pdf", 
                "İkinci test siparişi", "Ayşe Yılmaz", "0(555) 222 22 22", "ayse@example.com",
                "Merkez Mah. Atatürk Cad. No:15 Daire:3",
                "Merkez Mah. Atatürk Cad. No:15 Daire:3");
            
            // OrderLines verisi
            linesTable.Rows.Add(1, 0, "SKU001", "1-5 Yaş İçi Pamuklu Kışlık Erkek Çocuk Eşofman Takımı", 
                1, "29.9", "29.9", "TL", 8, "Kırmızı", "Beden", "3 Yaş");
            linesTable.Rows.Add(1, 1, "SKU002", "Çocuk Ayakkabısı", 
                2, "45.0", "90.0", "TL", 18, "Mavi", "Numara", "28");
            linesTable.Rows.Add(2, 2, "SKU003", "Kadın Elbise", 
                1, "120.0", "120.0", "TL", 18, "Siyah", "Beden", "M");
            linesTable.Rows.Add(2, 3, "SKU004", "Kadın Çanta", 
                1, "30.0", "30.0", "TL", 18, "Kahverengi", "Model", "Deri");
            
            // Tabloları DataSet'e ekle
            dataSet.Tables.Add(ordersTable);
            dataSet.Tables.Add(linesTable);
            
            // İlişki kur
            var relation = new System.Data.DataRelation("OrderLines", 
                ordersTable.Columns["id"], 
                linesTable.Columns["order_id"]);
            dataSet.Relations.Add(relation);
            
            // Grid'e bağla
            gridControlOrders.DataSource = ordersTable;
        }



        #endregion
        
        #region Sentos Stok Aktarımı

        /// <summary>
        /// Seçili ürünleri Sentos'a aktarır
        /// </summary>
        private async void btnTransferProducts_Click(object sender, EventArgs e)
        {
            await TransferProductsToSentosAsync();
        }

        private async Task TransferProductsToSentosAsync()
        {
            try
            {
                // Seçili satırları al
                var selectedRows = gridViewProducts.GetSelectedRows();
                if (selectedRows.Length == 0)
                {
                    XtraMessageBox.Show("Lütfen aktarılacak ürünleri seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                btnTransferProducts.Enabled = false;
                btnTransferProducts.Text = "Aktarılıyor...";

                int successCount = 0;
                int failCount = 0;
                var errorMessages = new List<string>();

                foreach (int rowHandle in selectedRows)
                {
                    var product = gridViewProducts.GetRow(rowHandle) as Product;
                    if (product == null) continue;

                    try
                    {
                        // Ürünün Sentos'ta var olup olmadığını kontrol et
                        var existingProduct = await _sentosService.GetProductBySkuAsync(product.SKU);
                        
                        ProductResponse result;
                        if (existingProduct.Success && existingProduct.Data != null)
                        {
                            // Ürün varsa güncelle
                            result = await _sentosService.UpdateProductAsync(existingProduct.Data.id, product);
                            product.TransferStatus = "Güncellendi";
                        }
                        else
                        {
                            // Ürün yoksa oluştur
                            result = await _sentosService.CreateProductAsync(product);
                            product.TransferStatus = "Oluşturuldu";
                        }

                        if (result.Success)
                        {
                            successCount++;
                            if (result.Data != null)
                            {
                                product.SentosId = result.Data.id;
                            }
                        }
                        else
                        {
                            failCount++;
                            errorMessages.Add($"{product.SKU}: {result.Message}");
                            product.TransferStatus = "Hata";
                        }
                    }
                    catch (Exception ex)
                    {
                        failCount++;
                        errorMessages.Add($"{product.SKU}: {ex.Message}");
                        product.TransferStatus = "Hata";
                    }
                }

                // Sonuçları göster
                string message = $"Aktarım tamamlandı.\n\nBaşarılı: {successCount}\nBaşarısız: {failCount}";
                
                if (failCount > 0)
                {
                    message += $"\n\nHatalar:\n{string.Join("\n", errorMessages.Take(5))}";
                    if (errorMessages.Count > 5)
                        message += $"\n... ve {errorMessages.Count - 5} hata daha";
                }

                XtraMessageBox.Show(message, "Aktarım Sonucu", MessageBoxButtons.OK,
                    failCount > 0 ? MessageBoxIcon.Warning : MessageBoxIcon.Information);

                // Grid'i yenile
                gridViewProducts.RefreshData();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Aktarım sırasında hata: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnTransferProducts.Enabled = true;
                btnTransferProducts.Text = "Seçili Ürünleri Aktar";
            }
        }

        private async Task TransferStockToSentosAsync()
        {
            try
            {
                var selectedRows = gridViewProducts.GetSelectedRows();
                if (selectedRows.Length == 0)
                {
                    XtraMessageBox.Show("Lütfen stok aktarılacak ürünleri seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var result = XtraMessageBox.Show($"{selectedRows.Length} ürünün stoku Sentos'a aktarılacak.\n\nDevam etmek istiyor musunuz?",
                    "Onay", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result != DialogResult.Yes)
                    return;

                btnTransferProducts.Enabled = false;
                btnTransferProducts.Text = "Stok Aktarılıyor...";

                int successCount = 0;
                int failCount = 0;
                var errorMessages = new List<string>();

                foreach (int rowHandle in selectedRows)
                {
                    var product = gridViewProducts.GetRow(rowHandle) as Product;
                    if (product != null)
                    {
                        try
                        {
                            // Stok aktarımı yap
                            var stockResult = await TransferProductStockAsync(product);
                            
                            if (stockResult.Success)
                            {
                                successCount++;
                                product.TransferStatus = "Stok Aktarıldı";
                            }
                            else
                            {
                                failCount++;
                                errorMessages.Add($"{product.SKU}: {stockResult.Message}");
                                product.TransferStatus = "Stok Hatası";
                            }
                        }
                        catch (Exception ex)
                        {
                            failCount++;
                            errorMessages.Add($"{product.SKU}: {ex.Message}");
                            product.TransferStatus = "Stok Hatası";
                        }
                    }
                }

                // Sonuçları göster
                string message = $"Stok aktarımı tamamlandı.\n\nBaşarılı: {successCount}\nBaşarısız: {failCount}";
                
                if (failCount > 0)
                {
                    message += $"\n\nHatalar:\n{string.Join("\n", errorMessages.Take(5))}";
                    if (errorMessages.Count > 5)
                        message += $"\n... ve {errorMessages.Count - 5} hata daha";
                }

                XtraMessageBox.Show(message, "Stok Aktarım Sonucu", MessageBoxButtons.OK,
                    failCount > 0 ? MessageBoxIcon.Warning : MessageBoxIcon.Information);

                // Grid'i yenile
                gridViewProducts.RefreshData();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Stok aktarımı sırasında hata: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnTransferProducts.Enabled = true;
                btnTransferProducts.Text = "Seçili Ürünleri Aktar";
            }
        }

        /// <summary>
        /// Tek bir ürünün stokunu Sentos'a aktarır
        /// </summary>
        private async Task<ProductResponse> TransferProductStockAsync(Product product)
        {
            try
            {
                // Önce ürünün Sentos'ta var olup olmadığını kontrol et
                var existingProduct = await _sentosService.GetProductBySkuAsync(product.SKU);
                
                if (existingProduct.Success && existingProduct.Data != null)
                {
                    // Ürün varsa stoklarını güncelle
                    var stockResult = await _sentosService.UpdateProductStockAsync(existingProduct.Data.Id, product.Stocks);
                    return stockResult;
                }
                else
                {
                    // Ürün yoksa önce oluştur, sonra stok aktar
                    var createResult = await _sentosService.CreateProductAsync(product);
                    return createResult;
                }
            }
            catch (Exception ex)
            {
                return new ProductResponse
                {
                    Success = false,
                    Message = $"Stok aktarım hatası: {ex.Message}",
                    Errors = new List<string> { ex.Message }
                };
            }
        }


        #endregion

        #region Sentos Sipariş İşlemleri

        /// <summary>
        /// Sentos siparişlerini listeler
        /// </summary>
        private async void btnRefreshOrders_Click(object sender, EventArgs e)
        {
            await ListSentosOrdersAsync();
        }

        private async Task ListSentosOrdersAsync()
        {
            try
            {
                btnRefreshOrders.Enabled = false;
                btnRefreshOrders.Text = "Yükleniyor...";

                // Siparişleri getir (Onay Bekliyor durumunda)
                var result = await _sentosService.GetOrdersAsync(1, 100, 1);

                if (result.Success)
                {
                    gridControlOrders.DataSource = result.Data;
                    XtraMessageBox.Show($"{result.Data.Count} Sentos siparişi listelendi.\n\nDurum: Onay Bekliyor (1)", 
                        "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    XtraMessageBox.Show($"Siparişler listelenemedi: {result.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Siparişler listelenirken hata: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnRefreshOrders.Enabled = true;
                btnRefreshOrders.Text = "Siparişleri Listele";
            }
        }

        /// <summary>
        /// Sipariş durum kodunu açıklamaya çevirir
        /// </summary>
        private string GetStatusName(int status)
        {
            switch (status)
            {
                case 1: return "Onay Bekliyor";
                case 2: return "Onaylandı";
                case 3: return "Tedarik Sürecinde";
                case 4: return "Hazırlanıyor";
                case 5: return "Kargoya Verildi";
                case 6: return "İptal Edildi";
                case 99: return "Teslim Edildi";
                default: return $"Bilinmeyen ({status})";
            }
        }


        #endregion

        #region Sentos API Test İşlemleri


        #endregion
        
 
    }
}
