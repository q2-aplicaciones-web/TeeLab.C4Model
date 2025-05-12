using Structurizr;

namespace TeeLab.C4Model;

public class ContainerDiagram
{
    private C4 Project { get; set; }
    private ContextDiagram Context { get; set; }
    
    public Container LandingPage { get; set; }
    public Container WebApp { get; set; }
    public Container Api { get; set; }
    
    public Container OrderProcessing { get; set; }
    
    public Container DesignLab { get; set; }
    public Container PaymentGateway { get; set; }
    public Container OrderFulfillment { get; set; }
    public Container ProductCatalog { get; set; }
    public Container UserManagement { get; set; }
    
    public Container SinglePageApplication { get; set; }

    
    public ContainerDiagram(ContextDiagram context, C4 project)
    {
        Context = context ?? throw new ArgumentNullException(nameof(context));
        Project = project ?? throw new ArgumentNullException(nameof(project));
        
        LandingPage = Context.TeeLab.AddContainer("Landing Page", "Initial HTML/CSS/JS entry point for the TeeLab platform.", "HTML, CSS, JS");
        LandingPage.AddTags(nameof(LandingPage));

        WebApp = Context.TeeLab.AddContainer("Web App", "Single-page frontend built with Vue.js 3 for user interaction and navigation.", "Vue.js 3");
        WebApp.AddTags(nameof(WebApp));

        Api = Context.TeeLab.AddContainer("API", "ASP.NET Core Web API that manages business logic, database access, and third-party integrations.", "ASP.NET Core, C#");
        Api.AddTags(nameof(Api));

        OrderProcessing = Context.TeeLab.AddContainer("Order Processing", "Handles order validation and workflow orchestration using ASP.NET Core.", "ASP.NET Core, C#");
        OrderProcessing.AddTags(nameof(OrderProcessing));

        DesignLab = Context.TeeLab.AddContainer("Design Studio", "Manages design creation, editing, and blueprint logic using ASP.NET Core.", "ASP.NET Core, C#");
        DesignLab.AddTags(nameof(DesignLab));

        PaymentGateway = Context.TeeLab.AddContainer("Payment Gateway", "Handles payment processing and integration with external services using ASP.NET Core.", "ASP.NET Core, C#");
        PaymentGateway.AddTags(nameof(PaymentGateway));

        OrderFulfillment = Context.TeeLab.AddContainer("Order Fulfillment", "Coordinates the production, packaging, and shipment of orders using ASP.NET Core.", "ASP.NET Core, C#");
        OrderFulfillment.AddTags(nameof(OrderFulfillment));

        ProductCatalog = Context.TeeLab.AddContainer("Product Catalog", "Provides product listings, filtering, and search using ASP.NET Core.", "ASP.NET Core, C#");
        ProductCatalog.AddTags(nameof(ProductCatalog));

        UserManagement = Context.TeeLab.AddContainer("User Management", "Manages user authentication, roles, and access control using ASP.NET Core.", "ASP.NET Core, C#");
        UserManagement.AddTags(nameof(UserManagement));

        SinglePageApplication = Context.TeeLab.AddContainer("Single Page Application", "SPA frontend built with Vue.js 3 to handle routing and dynamic views.", "Vue.js 3");
        SinglePageApplication.AddTags(nameof(SinglePageApplication));

    }

    public void Generate()
    {
        Context.User.Uses(LandingPage, "Accesses the landing page to explore the platform.");
        Context.Designer.Uses(WebApp, "Uses the Vue.js web app to create and manage garment designs.");
        Context.Manufacturer.Uses(WebApp, "Uses the Vue.js web app to manage production tasks.");

        Api.Uses(Context.Cloudinary, "Connects to Cloudinary for image storage and delivery.");
        Api.Uses(Context.Stripe, "Connects to Stripe to handle secure payment transactions.");
        Api.Uses(Context.Supabase, "Connects to Supabase for data storage and real-time access.");
        SinglePageApplication.Uses(Context.Cloudinary, "Connects to Cloudinary for image storage and delivery.");

        LandingPage.Uses(WebApp, "Redirects users to the Vue.js web application.");
        WebApp.Uses(SinglePageApplication, "Loads and runs the Vue.js single-page application interface.");
        SinglePageApplication.Uses(Api, "Communicates with the ASP.NET Core API to retrieve and manage data.");

        
        ApplyStyles();
        Publish();
    }

    private void ApplyStyles()
    {
        var styles = Project.ViewSet.Configuration.Styles;
        
        styles.Add(new ElementStyle(nameof(LandingPage)) {Background = "#006A1C", Shape = Shape.RoundedBox, Color = "#FFFFFF"});
        styles.Add(new ElementStyle(nameof(WebApp)) {Background = "#D96030", Shape = Shape.RoundedBox, Color = "#FFFFFF"});
        styles.Add(new ElementStyle(nameof(Api)) {Background = "#FF0D17", Shape = Shape.RoundedBox, Color = "#FFFFFF"});
        styles.Add(new ElementStyle(nameof(SinglePageApplication)) { Background = "#408dd5", Shape = Shape.WebBrowser, Color = "#FFFFFF" });;
    }
    
    private void Publish()
    {
        ContainerView view = Project.ViewSet.CreateContainerView(Context.TeeLab, "TeeLab Container View", "");
        view.AddAllSoftwareSystems();
        view.AddAllPeople();
        view.Add(LandingPage);
        view.Add(WebApp);
        view.Add(Api);
        view.Add(SinglePageApplication);
    }
}