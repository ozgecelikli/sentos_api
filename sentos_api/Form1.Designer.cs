namespace sentos_api
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            DevExpress.XtraGrid.GridLevelNode gridLevelNode1 = new DevExpress.XtraGrid.GridLevelNode();
            this.gridControlOrders = new DevExpress.XtraGrid.GridControl();
            this.gridViewOrders = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colOrderId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colOrderIdSentos = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colOrderNumber = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colOrderCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSource = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colShop = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colOrderDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTotalAmount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colShippingTotal = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCargoProvider = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCargoNumber = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colHasInvoice = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colInvoiceType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colInvoiceNumber = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colInvoiceUrl = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNote = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCustomerName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCustomerPhone = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCustomerEmail = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colInvoiceAddress = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colShipmentAddress = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colOrderStatus = new DevExpress.XtraGrid.Columns.GridColumn();
            this.tabControl1 = new DevExpress.XtraTab.XtraTabControl();
            this.tabPageProducts = new DevExpress.XtraTab.XtraTabPage();
            this.btnTransferProducts = new DevExpress.XtraEditors.SimpleButton();
            this.btnRefreshProducts = new DevExpress.XtraEditors.SimpleButton();
            this.gridControlProducts = new DevExpress.XtraGrid.GridControl();
            this.gridViewProducts = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDescription = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSKU = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPrice = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSalePrice = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colStock = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCategory = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBrand = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colStatus = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colWeight = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDimensions = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBarcode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTaxRate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colIsActive = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMetaTitle = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMetaDescription = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTags = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMinStock = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMaxStock = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colUnit = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSupplier = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNotes = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCreatedDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colUpdatedDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTransferStatus = new DevExpress.XtraGrid.Columns.GridColumn();
            this.tabPageOrders = new DevExpress.XtraTab.XtraTabPage();
            this.btnTransferOrders = new DevExpress.XtraEditors.SimpleButton();
            this.btnRefreshOrders = new DevExpress.XtraEditors.SimpleButton();
            this.gridViewOrderLines = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlOrders)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewOrders)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabControl1)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPageProducts.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlProducts)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewProducts)).BeginInit();
            this.tabPageOrders.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewOrderLines)).BeginInit();
            this.SuspendLayout();
            // 
            // gridControlOrders
            // 
            this.gridControlOrders.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            gridLevelNode1.LevelTemplate = this.gridViewOrderLines;
            gridLevelNode1.RelationName = "Level1";
            this.gridControlOrders.LevelTree.Nodes.AddRange(new DevExpress.XtraGrid.GridLevelNode[] {
            gridLevelNode1});
            this.gridControlOrders.Location = new System.Drawing.Point(10, 50);
            this.gridControlOrders.MainView = this.gridViewOrders;
            this.gridControlOrders.Name = "gridControlOrders";
            this.gridControlOrders.Size = new System.Drawing.Size(1174, 511);
            this.gridControlOrders.TabIndex = 0;
            this.gridControlOrders.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewOrders,
            this.gridViewOrderLines});
            // 
            // gridViewOrders
            // 
            this.gridViewOrders.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colOrderId,
            this.colOrderIdSentos,
            this.colOrderNumber,
            this.colOrderCode,
            this.colSource,
            this.colShop,
            this.colOrderDate,
            this.colTotalAmount,
            this.colShippingTotal,
            this.colCargoProvider,
            this.colCargoNumber,
            this.colHasInvoice,
            this.colInvoiceType,
            this.colInvoiceNumber,
            this.colInvoiceUrl,
            this.colNote,
            this.colCustomerName,
            this.colCustomerPhone,
            this.colCustomerEmail,
            this.colInvoiceAddress,
            this.colShipmentAddress,
            this.colOrderStatus});
            this.gridViewOrders.GridControl = this.gridControlOrders;
            this.gridViewOrders.Name = "gridViewOrders";
            this.gridViewOrders.OptionsDetail.AllowExpandEmptyDetails = true;
            this.gridViewOrders.OptionsDetail.EnableMasterViewMode = false;
            this.gridViewOrders.OptionsDetail.ShowDetailTabs = false;
            this.gridViewOrders.OptionsSelection.MultiSelect = true;
            this.gridViewOrders.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            // 
            // colOrderId
            // 
            this.colOrderId.Caption = "ID";
            this.colOrderId.FieldName = "id";
            this.colOrderId.Name = "colOrderId";
            this.colOrderId.Visible = true;
            this.colOrderId.VisibleIndex = 1;
            this.colOrderId.Width = 60;
            // 
            // colOrderIdSentos
            // 
            this.colOrderIdSentos.Caption = "SentOS ID";
            this.colOrderIdSentos.FieldName = "order_id";
            this.colOrderIdSentos.Name = "colOrderIdSentos";
            this.colOrderIdSentos.Visible = true;
            this.colOrderIdSentos.VisibleIndex = 2;
            this.colOrderIdSentos.Width = 100;
            // 
            // colOrderNumber
            // 
            this.colOrderNumber.Caption = "Sipariş No";
            this.colOrderNumber.FieldName = "order_code";
            this.colOrderNumber.Name = "colOrderNumber";
            this.colOrderNumber.Visible = true;
            this.colOrderNumber.VisibleIndex = 3;
            this.colOrderNumber.Width = 150;
            // 
            // colOrderCode
            // 
            this.colOrderCode.Caption = "Sipariş Kodu";
            this.colOrderCode.FieldName = "order_code";
            this.colOrderCode.Name = "colOrderCode";
            this.colOrderCode.Visible = true;
            this.colOrderCode.VisibleIndex = 4;
            this.colOrderCode.Width = 150;
            // 
            // colSource
            // 
            this.colSource.Caption = "Kaynak";
            this.colSource.FieldName = "source";
            this.colSource.Name = "colSource";
            this.colSource.Visible = true;
            this.colSource.VisibleIndex = 5;
            this.colSource.Width = 80;
            // 
            // colShop
            // 
            this.colShop.Caption = "Mağaza";
            this.colShop.FieldName = "shop";
            this.colShop.Name = "colShop";
            this.colShop.Visible = true;
            this.colShop.VisibleIndex = 6;
            this.colShop.Width = 120;
            // 
            // colOrderDate
            // 
            this.colOrderDate.Caption = "Sipariş Tarihi";
            this.colOrderDate.FieldName = "order_date";
            this.colOrderDate.Name = "colOrderDate";
            this.colOrderDate.Visible = true;
            this.colOrderDate.VisibleIndex = 7;
            this.colOrderDate.Width = 120;
            // 
            // colTotalAmount
            // 
            this.colTotalAmount.Caption = "Toplam Tutar";
            this.colTotalAmount.FieldName = "total";
            this.colTotalAmount.Name = "colTotalAmount";
            this.colTotalAmount.Visible = true;
            this.colTotalAmount.VisibleIndex = 8;
            this.colTotalAmount.Width = 100;
            // 
            // colShippingTotal
            // 
            this.colShippingTotal.Caption = "Kargo Tutarı";
            this.colShippingTotal.FieldName = "shipping_total";
            this.colShippingTotal.Name = "colShippingTotal";
            this.colShippingTotal.Visible = true;
            this.colShippingTotal.VisibleIndex = 9;
            this.colShippingTotal.Width = 100;
            // 
            // colCargoProvider
            // 
            this.colCargoProvider.Caption = "Kargo Firması";
            this.colCargoProvider.FieldName = "cargo_provider";
            this.colCargoProvider.Name = "colCargoProvider";
            this.colCargoProvider.Visible = true;
            this.colCargoProvider.VisibleIndex = 10;
            this.colCargoProvider.Width = 120;
            // 
            // colCargoNumber
            // 
            this.colCargoNumber.Caption = "Kargo No";
            this.colCargoNumber.FieldName = "cargo_number";
            this.colCargoNumber.Name = "colCargoNumber";
            this.colCargoNumber.Visible = true;
            this.colCargoNumber.VisibleIndex = 11;
            this.colCargoNumber.Width = 120;
            // 
            // colHasInvoice
            // 
            this.colHasInvoice.Caption = "Fatura Var";
            this.colHasInvoice.FieldName = "has_invoice";
            this.colHasInvoice.Name = "colHasInvoice";
            this.colHasInvoice.Visible = true;
            this.colHasInvoice.VisibleIndex = 12;
            this.colHasInvoice.Width = 80;
            // 
            // colInvoiceType
            // 
            this.colInvoiceType.Caption = "Fatura Tipi";
            this.colInvoiceType.FieldName = "invoice_type";
            this.colInvoiceType.Name = "colInvoiceType";
            this.colInvoiceType.Visible = true;
            this.colInvoiceType.VisibleIndex = 13;
            this.colInvoiceType.Width = 100;
            // 
            // colInvoiceNumber
            // 
            this.colInvoiceNumber.Caption = "Fatura No";
            this.colInvoiceNumber.FieldName = "invoice_number";
            this.colInvoiceNumber.Name = "colInvoiceNumber";
            this.colInvoiceNumber.Visible = true;
            this.colInvoiceNumber.VisibleIndex = 14;
            this.colInvoiceNumber.Width = 120;
            // 
            // colInvoiceUrl
            // 
            this.colInvoiceUrl.Caption = "Fatura URL";
            this.colInvoiceUrl.FieldName = "invoice_url";
            this.colInvoiceUrl.Name = "colInvoiceUrl";
            this.colInvoiceUrl.Visible = true;
            this.colInvoiceUrl.VisibleIndex = 15;
            this.colInvoiceUrl.Width = 200;
            // 
            // colNote
            // 
            this.colNote.Caption = "Not";
            this.colNote.FieldName = "note";
            this.colNote.Name = "colNote";
            this.colNote.Visible = true;
            this.colNote.VisibleIndex = 16;
            this.colNote.Width = 150;
            // 
            // colCustomerName
            // 
            this.colCustomerName.Caption = "Müşteri Adı";
            this.colCustomerName.FieldName = "customer_name";
            this.colCustomerName.Name = "colCustomerName";
            this.colCustomerName.Visible = true;
            this.colCustomerName.VisibleIndex = 17;
            this.colCustomerName.Width = 150;
            // 
            // colCustomerPhone
            // 
            this.colCustomerPhone.Caption = "Müşteri Telefon";
            this.colCustomerPhone.FieldName = "customer_phone";
            this.colCustomerPhone.Name = "colCustomerPhone";
            this.colCustomerPhone.Visible = true;
            this.colCustomerPhone.VisibleIndex = 18;
            this.colCustomerPhone.Width = 120;
            // 
            // colCustomerEmail
            // 
            this.colCustomerEmail.Caption = "Müşteri Email";
            this.colCustomerEmail.FieldName = "customer_email";
            this.colCustomerEmail.Name = "colCustomerEmail";
            this.colCustomerEmail.Visible = true;
            this.colCustomerEmail.VisibleIndex = 19;
            this.colCustomerEmail.Width = 150;
            // 
            // colInvoiceAddress
            // 
            this.colInvoiceAddress.Caption = "Fatura Adresi";
            this.colInvoiceAddress.FieldName = "invoice_address";
            this.colInvoiceAddress.Name = "colInvoiceAddress";
            this.colInvoiceAddress.Visible = true;
            this.colInvoiceAddress.VisibleIndex = 20;
            this.colInvoiceAddress.Width = 200;
            // 
            // colShipmentAddress
            // 
            this.colShipmentAddress.Caption = "Teslimat Adresi";
            this.colShipmentAddress.FieldName = "shipment_address";
            this.colShipmentAddress.Name = "colShipmentAddress";
            this.colShipmentAddress.Visible = true;
            this.colShipmentAddress.VisibleIndex = 21;
            this.colShipmentAddress.Width = 200;
            // 
            // colOrderStatus
            // 
            this.colOrderStatus.Caption = "Durum";
            this.colOrderStatus.FieldName = "status";
            this.colOrderStatus.Name = "colOrderStatus";
            this.colOrderStatus.Visible = true;
            this.colOrderStatus.VisibleIndex = 22;
            this.colOrderStatus.Width = 80;
            // 
            // tabControl1
            // 
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedTabPage = this.tabPageProducts;
            this.tabControl1.Size = new System.Drawing.Size(1200, 600);
            this.tabControl1.TabIndex = 0;
            this.tabControl1.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.tabPageProducts,
            this.tabPageOrders});
            // 
            // tabPageProducts
            // 
            this.tabPageProducts.Controls.Add(this.btnTransferProducts);
            this.tabPageProducts.Controls.Add(this.btnRefreshProducts);
            this.tabPageProducts.Controls.Add(this.gridControlProducts);
            this.tabPageProducts.Name = "tabPageProducts";
            this.tabPageProducts.Size = new System.Drawing.Size(1198, 570);
            this.tabPageProducts.Text = "Ürün Aktarımı";
            // 
            // btnTransferProducts
            // 
            this.btnTransferProducts.Location = new System.Drawing.Point(150, 10);
            this.btnTransferProducts.Name = "btnTransferProducts";
            this.btnTransferProducts.Size = new System.Drawing.Size(120, 30);
            this.btnTransferProducts.TabIndex = 2;
            this.btnTransferProducts.Text = "Seçili Ürünleri Aktar";
            this.btnTransferProducts.Click += new System.EventHandler(this.btnTransferProducts_Click);
            // 
            // btnRefreshProducts
            // 
            this.btnRefreshProducts.Location = new System.Drawing.Point(10, 10);
            this.btnRefreshProducts.Name = "btnRefreshProducts";
            this.btnRefreshProducts.Size = new System.Drawing.Size(120, 30);
            this.btnRefreshProducts.TabIndex = 1;
            this.btnRefreshProducts.Text = "Ürünleri Listele";
            this.btnRefreshProducts.Click += new System.EventHandler(this.btnRefreshProducts_Click);
            // 
            // gridControlProducts
            // 
            this.gridControlProducts.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridControlProducts.Location = new System.Drawing.Point(10, 50);
            this.gridControlProducts.MainView = this.gridViewProducts;
            this.gridControlProducts.Name = "gridControlProducts";
            this.gridControlProducts.Size = new System.Drawing.Size(1174, 511);
            this.gridControlProducts.TabIndex = 0;
            this.gridControlProducts.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewProducts});
            // 
            // gridViewProducts
            // 
            this.gridViewProducts.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colId,
            this.colName,
            this.colDescription,
            this.colSKU,
            this.colPrice,
            this.colSalePrice,
            this.colStock,
            this.colCategory,
            this.colBrand,
            this.colStatus,
            this.colWeight,
            this.colDimensions,
            this.colBarcode,
            this.colTaxRate,
            this.colIsActive,
            this.colMetaTitle,
            this.colMetaDescription,
            this.colTags,
            this.colMinStock,
            this.colMaxStock,
            this.colUnit,
            this.colSupplier,
            this.colNotes,
            this.colCreatedDate,
            this.colUpdatedDate,
            this.colTransferStatus});
            this.gridViewProducts.GridControl = this.gridControlProducts;
            this.gridViewProducts.Name = "gridViewProducts";
            this.gridViewProducts.OptionsSelection.MultiSelect = true;
            this.gridViewProducts.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            // 
            // colId
            // 
            this.colId.Caption = "ID";
            this.colId.FieldName = "Id";
            this.colId.Name = "colId";
            this.colId.Visible = true;
            this.colId.VisibleIndex = 1;
            this.colId.Width = 60;
            // 
            // colName
            // 
            this.colName.Caption = "Ürün Adı";
            this.colName.FieldName = "Name";
            this.colName.Name = "colName";
            this.colName.Visible = true;
            this.colName.VisibleIndex = 2;
            this.colName.Width = 200;
            // 
            // colDescription
            // 
            this.colDescription.Caption = "Açıklama";
            this.colDescription.FieldName = "Description";
            this.colDescription.Name = "colDescription";
            this.colDescription.Visible = true;
            this.colDescription.VisibleIndex = 3;
            this.colDescription.Width = 250;
            // 
            // colSKU
            // 
            this.colSKU.Caption = "Stok Kodu";
            this.colSKU.FieldName = "SKU";
            this.colSKU.Name = "colSKU";
            this.colSKU.Visible = true;
            this.colSKU.VisibleIndex = 4;
            this.colSKU.Width = 120;
            // 
            // colPrice
            // 
            this.colPrice.Caption = "Fiyat";
            this.colPrice.FieldName = "Price";
            this.colPrice.Name = "colPrice";
            this.colPrice.Visible = true;
            this.colPrice.VisibleIndex = 5;
            this.colPrice.Width = 80;
            // 
            // colSalePrice
            // 
            this.colSalePrice.Caption = "Satış Fiyatı";
            this.colSalePrice.FieldName = "SalePrice";
            this.colSalePrice.Name = "colSalePrice";
            this.colSalePrice.Visible = true;
            this.colSalePrice.VisibleIndex = 6;
            this.colSalePrice.Width = 80;
            // 
            // colStock
            // 
            this.colStock.Caption = "Stok";
            this.colStock.FieldName = "Stock";
            this.colStock.Name = "colStock";
            this.colStock.Visible = true;
            this.colStock.VisibleIndex = 7;
            this.colStock.Width = 60;
            // 
            // colCategory
            // 
            this.colCategory.Caption = "Kategori";
            this.colCategory.FieldName = "Category";
            this.colCategory.Name = "colCategory";
            this.colCategory.Visible = true;
            this.colCategory.VisibleIndex = 8;
            this.colCategory.Width = 120;
            // 
            // colBrand
            // 
            this.colBrand.Caption = "Marka";
            this.colBrand.FieldName = "Brand";
            this.colBrand.Name = "colBrand";
            this.colBrand.Visible = true;
            this.colBrand.VisibleIndex = 9;
            this.colBrand.Width = 100;
            // 
            // colStatus
            // 
            this.colStatus.Caption = "Durum";
            this.colStatus.FieldName = "Status";
            this.colStatus.Name = "colStatus";
            this.colStatus.Visible = true;
            this.colStatus.VisibleIndex = 10;
            this.colStatus.Width = 80;
            // 
            // colWeight
            // 
            this.colWeight.Caption = "Ağırlık";
            this.colWeight.FieldName = "Weight";
            this.colWeight.Name = "colWeight";
            this.colWeight.Visible = true;
            this.colWeight.VisibleIndex = 11;
            this.colWeight.Width = 80;
            // 
            // colDimensions
            // 
            this.colDimensions.Caption = "Boyutlar";
            this.colDimensions.FieldName = "Dimensions";
            this.colDimensions.Name = "colDimensions";
            this.colDimensions.Visible = true;
            this.colDimensions.VisibleIndex = 12;
            this.colDimensions.Width = 100;
            // 
            // colBarcode
            // 
            this.colBarcode.Caption = "Barkod";
            this.colBarcode.FieldName = "Barcode";
            this.colBarcode.Name = "colBarcode";
            this.colBarcode.Visible = true;
            this.colBarcode.VisibleIndex = 13;
            this.colBarcode.Width = 120;
            // 
            // colTaxRate
            // 
            this.colTaxRate.Caption = "Vergi Oranı";
            this.colTaxRate.FieldName = "TaxRate";
            this.colTaxRate.Name = "colTaxRate";
            this.colTaxRate.Visible = true;
            this.colTaxRate.VisibleIndex = 14;
            this.colTaxRate.Width = 80;
            // 
            // colIsActive
            // 
            this.colIsActive.Caption = "Aktif";
            this.colIsActive.FieldName = "IsActive";
            this.colIsActive.Name = "colIsActive";
            this.colIsActive.Visible = true;
            this.colIsActive.VisibleIndex = 15;
            this.colIsActive.Width = 60;
            // 
            // colMetaTitle
            // 
            this.colMetaTitle.Caption = "Meta Başlık";
            this.colMetaTitle.FieldName = "MetaTitle";
            this.colMetaTitle.Name = "colMetaTitle";
            this.colMetaTitle.Visible = true;
            this.colMetaTitle.VisibleIndex = 16;
            this.colMetaTitle.Width = 150;
            // 
            // colMetaDescription
            // 
            this.colMetaDescription.Caption = "Meta Açıklama";
            this.colMetaDescription.FieldName = "MetaDescription";
            this.colMetaDescription.Name = "colMetaDescription";
            this.colMetaDescription.Visible = true;
            this.colMetaDescription.VisibleIndex = 17;
            this.colMetaDescription.Width = 200;
            // 
            // colTags
            // 
            this.colTags.Caption = "Etiketler";
            this.colTags.FieldName = "Tags";
            this.colTags.Name = "colTags";
            this.colTags.Visible = true;
            this.colTags.VisibleIndex = 18;
            this.colTags.Width = 150;
            // 
            // colMinStock
            // 
            this.colMinStock.Caption = "Min Stok";
            this.colMinStock.FieldName = "MinStock";
            this.colMinStock.Name = "colMinStock";
            this.colMinStock.Visible = true;
            this.colMinStock.VisibleIndex = 19;
            this.colMinStock.Width = 70;
            // 
            // colMaxStock
            // 
            this.colMaxStock.Caption = "Max Stok";
            this.colMaxStock.FieldName = "MaxStock";
            this.colMaxStock.Name = "colMaxStock";
            this.colMaxStock.Visible = true;
            this.colMaxStock.VisibleIndex = 20;
            this.colMaxStock.Width = 70;
            // 
            // colUnit
            // 
            this.colUnit.Caption = "Birim";
            this.colUnit.FieldName = "Unit";
            this.colUnit.Name = "colUnit";
            this.colUnit.Visible = true;
            this.colUnit.VisibleIndex = 21;
            this.colUnit.Width = 60;
            // 
            // colSupplier
            // 
            this.colSupplier.Caption = "Tedarikçi";
            this.colSupplier.FieldName = "Supplier";
            this.colSupplier.Name = "colSupplier";
            this.colSupplier.Visible = true;
            this.colSupplier.VisibleIndex = 22;
            this.colSupplier.Width = 120;
            // 
            // colNotes
            // 
            this.colNotes.Caption = "Notlar";
            this.colNotes.FieldName = "Notes";
            this.colNotes.Name = "colNotes";
            this.colNotes.Visible = true;
            this.colNotes.VisibleIndex = 23;
            this.colNotes.Width = 150;
            // 
            // colCreatedDate
            // 
            this.colCreatedDate.Caption = "Oluşturma Tarihi";
            this.colCreatedDate.FieldName = "CreatedDate";
            this.colCreatedDate.Name = "colCreatedDate";
            this.colCreatedDate.Visible = true;
            this.colCreatedDate.VisibleIndex = 24;
            this.colCreatedDate.Width = 120;
            // 
            // colUpdatedDate
            // 
            this.colUpdatedDate.Caption = "Güncelleme Tarihi";
            this.colUpdatedDate.FieldName = "UpdatedDate";
            this.colUpdatedDate.Name = "colUpdatedDate";
            this.colUpdatedDate.Visible = true;
            this.colUpdatedDate.VisibleIndex = 25;
            this.colUpdatedDate.Width = 120;
            // 
            // colTransferStatus
            // 
            this.colTransferStatus.Caption = "Aktarım Durumu";
            this.colTransferStatus.FieldName = "TransferStatus";
            this.colTransferStatus.Name = "colTransferStatus";
            this.colTransferStatus.Visible = true;
            this.colTransferStatus.VisibleIndex = 26;
            this.colTransferStatus.Width = 120;
            // 
            // tabPageOrders
            // 
            this.tabPageOrders.Controls.Add(this.btnTransferOrders);
            this.tabPageOrders.Controls.Add(this.btnRefreshOrders);
            this.tabPageOrders.Controls.Add(this.gridControlOrders);
            this.tabPageOrders.Name = "tabPageOrders";
            this.tabPageOrders.Size = new System.Drawing.Size(1198, 570);
            this.tabPageOrders.Text = "Sipariş Aktarımı";
            // 
            // btnTransferOrders
            // 
            this.btnTransferOrders.Location = new System.Drawing.Point(150, 10);
            this.btnTransferOrders.Name = "btnTransferOrders";
            this.btnTransferOrders.Size = new System.Drawing.Size(120, 30);
            this.btnTransferOrders.TabIndex = 2;
            this.btnTransferOrders.Text = "Seçili Siparişleri Aktar";
            this.btnTransferOrders.Click += new System.EventHandler(this.btnTransferOrders_Click);
            // 
            // btnRefreshOrders
            // 
            this.btnRefreshOrders.Location = new System.Drawing.Point(10, 10);
            this.btnRefreshOrders.Name = "btnRefreshOrders";
            this.btnRefreshOrders.Size = new System.Drawing.Size(120, 30);
            this.btnRefreshOrders.TabIndex = 1;
            this.btnRefreshOrders.Text = "Siparişleri Listele";
            this.btnRefreshOrders.Click += new System.EventHandler(this.btnRefreshOrders_Click);
            // 
            // gridViewOrderLines
            // 
            this.gridViewOrderLines.GridControl = this.gridControlOrders;
            this.gridViewOrderLines.Name = "gridViewOrderLines";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1200, 600);
            this.Controls.Add(this.tabControl1);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Form1";
            this.Text = "ERPiS - Sentos Entegrasyonu";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridControlOrders)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewOrders)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabControl1)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPageProducts.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControlProducts)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewProducts)).EndInit();
            this.tabPageOrders.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridViewOrderLines)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraTab.XtraTabControl tabControl1;
        private DevExpress.XtraTab.XtraTabPage tabPageProducts;
        private DevExpress.XtraTab.XtraTabPage tabPageOrders;
        private DevExpress.XtraEditors.SimpleButton btnRefreshProducts;
        private DevExpress.XtraGrid.GridControl gridControlProducts;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewProducts;
        private DevExpress.XtraGrid.Columns.GridColumn colId;
        private DevExpress.XtraGrid.Columns.GridColumn colName;
        private DevExpress.XtraGrid.Columns.GridColumn colSKU;
        private DevExpress.XtraGrid.Columns.GridColumn colPrice;
        private DevExpress.XtraGrid.Columns.GridColumn colStock;
        private DevExpress.XtraGrid.Columns.GridColumn colTransferStatus;
        private DevExpress.XtraGrid.Columns.GridColumn colDescription;
        private DevExpress.XtraGrid.Columns.GridColumn colSalePrice;
        private DevExpress.XtraGrid.Columns.GridColumn colCategory;
        private DevExpress.XtraGrid.Columns.GridColumn colBrand;
        private DevExpress.XtraGrid.Columns.GridColumn colStatus;
        private DevExpress.XtraGrid.Columns.GridColumn colWeight;
        private DevExpress.XtraGrid.Columns.GridColumn colDimensions;
        private DevExpress.XtraGrid.Columns.GridColumn colBarcode;
        private DevExpress.XtraGrid.Columns.GridColumn colTaxRate;
        private DevExpress.XtraGrid.Columns.GridColumn colIsActive;
        private DevExpress.XtraGrid.Columns.GridColumn colMetaTitle;
        private DevExpress.XtraGrid.Columns.GridColumn colMetaDescription;
        private DevExpress.XtraGrid.Columns.GridColumn colTags;
        private DevExpress.XtraGrid.Columns.GridColumn colMinStock;
        private DevExpress.XtraGrid.Columns.GridColumn colMaxStock;
        private DevExpress.XtraGrid.Columns.GridColumn colUnit;
        private DevExpress.XtraGrid.Columns.GridColumn colSupplier;
        private DevExpress.XtraGrid.Columns.GridColumn colNotes;
        private DevExpress.XtraGrid.Columns.GridColumn colCreatedDate;
        private DevExpress.XtraGrid.Columns.GridColumn colUpdatedDate;
        private DevExpress.XtraEditors.SimpleButton btnTransferProducts;
        private DevExpress.XtraEditors.SimpleButton btnRefreshOrders;
        private DevExpress.XtraGrid.GridControl gridControlOrders;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewOrders;
        //private DevExpress.XtraGrid.Views.Grid.GridView gridViewOrderLines;
        private DevExpress.XtraGrid.Columns.GridColumn colOrderId;
        private DevExpress.XtraGrid.Columns.GridColumn colOrderNumber;
        private DevExpress.XtraGrid.Columns.GridColumn colOrderDate;
        private DevExpress.XtraGrid.Columns.GridColumn colTotalAmount;
        private DevExpress.XtraGrid.Columns.GridColumn colOrderStatus;
        private DevExpress.XtraGrid.Columns.GridColumn colOrderIdSentos;
        private DevExpress.XtraGrid.Columns.GridColumn colOrderCode;
        private DevExpress.XtraGrid.Columns.GridColumn colSource;
        private DevExpress.XtraGrid.Columns.GridColumn colShop;
        private DevExpress.XtraGrid.Columns.GridColumn colShippingTotal;
        private DevExpress.XtraGrid.Columns.GridColumn colCargoProvider;
        private DevExpress.XtraGrid.Columns.GridColumn colCargoNumber;
        private DevExpress.XtraGrid.Columns.GridColumn colHasInvoice;
        private DevExpress.XtraGrid.Columns.GridColumn colInvoiceType;
        private DevExpress.XtraGrid.Columns.GridColumn colInvoiceNumber;
        private DevExpress.XtraGrid.Columns.GridColumn colInvoiceUrl;
        private DevExpress.XtraGrid.Columns.GridColumn colNote;
        private DevExpress.XtraGrid.Columns.GridColumn colCustomerName;
        private DevExpress.XtraGrid.Columns.GridColumn colCustomerPhone;
        private DevExpress.XtraGrid.Columns.GridColumn colCustomerEmail;
        private DevExpress.XtraGrid.Columns.GridColumn colInvoiceAddress;
        private DevExpress.XtraGrid.Columns.GridColumn colShipmentAddress;
        private DevExpress.XtraEditors.SimpleButton btnTransferOrders;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewOrderLines;
    }
}