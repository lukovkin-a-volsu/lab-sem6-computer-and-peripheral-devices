namespace Laboratory_Report_4;

public partial class Form1 : Form
{
    private const string Title = "Информация о процессоре и видеокарте";
    private const string VideoAdapterTitle = "Процессор";
    private const string ProcessorTitle = "Видеоадаптер";

    private const int PanelWidth = 550;
    private const int PanelHeight = 250;
    private const int Spacing = 10;
    private const int WindowWidth = Spacing + PanelWidth + Spacing;
    private const int WindowHeight =  Spacing + PanelHeight + Spacing + PanelHeight + Spacing;

    private Panel? _processorPanel;
    private Panel? _videoAdapterPanel;

    public Form1()
    {
        InitializeComponent();
        ConfigureForm();
        CreatePanels();
        LoadData();
    }

    private void ConfigureForm()
    {
        ClientSize = new Size(WindowWidth, WindowHeight);
        Text = Title;
        BackColor = Color.White;
    }

    private void CreatePanels()
    {
        _processorPanel = CreateInfoPanel(ProcessorTitle, Spacing);
        Controls.Add(_processorPanel);

        _videoAdapterPanel = CreateInfoPanel(VideoAdapterTitle, Spacing + PanelHeight + Spacing);
        Controls.Add(_videoAdapterPanel);
    }

    private static Panel CreateInfoPanel(string title, int top)
    {
        return new Panel
        {
            Location = new Point(Spacing, top),
            Size = new Size(PanelWidth, PanelHeight),
            BorderStyle = BorderStyle.FixedSingle,
            AutoScroll = true,
            Tag = title
        };
    }

    private void LoadData()
    {
        LoadProcessorInfo();
        LoadVideoAdapterInfo();
    }

    private void LoadProcessorInfo()
    {
        if (_processorPanel is null)
        {
            return;
        }
        
        var provider = new ProcessorInfoProvider();
        var yPos = 35;

        AddSectionTitle(_processorPanel, ProcessorTitle, 10);

        foreach (var info in provider.GetInfo())
        {
            AddLabel(_processorPanel, $"Модель: {info.Model ?? "N/A"}", yPos);
            AddLabel(_processorPanel, $"Количество ядер: {info.Cores ?? "N/A"}", yPos += 25);
            AddLabel(_processorPanel, $"Максимальная частота: {info.MaxFrequencyGHz?.ToString("N1") ?? "N/A"} ГГц", yPos += 25);
            AddLabel(_processorPanel, $"Кэш второго уровня (L2): {info.L2CacheKb ?? "N/A"} КБ", yPos += 25);
            AddLabel(_processorPanel, $"Кэш третьего уровня (L3): {info.L3CacheKb ?? "N/A"} КБ", yPos += 25);
        }
    }

    private void LoadVideoAdapterInfo()
    {
        if (_videoAdapterPanel is null)
        {
            return;
        }
        
        var provider = new VideoAdapterInfoProvider();
        var yPos = 35;

        AddSectionTitle(_videoAdapterPanel, VideoAdapterTitle, 10);

        foreach (var info in provider.GetInfo())
        {
            AddLabel(_videoAdapterPanel, $"Модель видеоадаптера: {info.Model ?? "N/A"}", yPos);
            //AddLabel(_videoAdapterPanel, $"Объем видеопамяти: {info.MemoryGb?.ToString("N2") ?? "N/A"} ГБ", yPos += 25);

            if (info.MemoryGb != null)
            {
                AddLabel(_videoAdapterPanel, $"Объем видеопамяти: {info.MemoryGb.ToString()} ГБ", yPos += 25);
            }
            else 
            {
                AddLabel(_videoAdapterPanel, "N/A", yPos += 25);
            }
            
            AddLabel(_videoAdapterPanel, $"Текущее разрешение: {info.Resolution ?? "N/A"}", yPos += 25);
            AddLabel(_videoAdapterPanel, $"Частота обновления: {info.RefreshRateHz ?? "N/A"} Гц", yPos += 25);
            AddLabel(_videoAdapterPanel, $"Идентификатор устройства: {info.DeviceId ?? "N/A"}", yPos += 25);
        }
    }

    private static void AddSectionTitle(Panel panel, string text, int y)
    {
        var label = new Label
        {
            Text = text,
            Font = new Font("Segoe UI", 10, FontStyle.Bold),
            ForeColor = Color.CornflowerBlue,
            Location = new Point(15, y),
            AutoSize = true
        };
        panel.Controls.Add(label);
    }

    private static void AddLabel(Panel panel, string text, int y)
    {
        var label = new Label
        {
            Text = text,
            Font = new Font("Segoe UI", 9),
            Location = new Point(15, y),
            AutoSize = true
        };
        panel.Controls.Add(label);
    }
}