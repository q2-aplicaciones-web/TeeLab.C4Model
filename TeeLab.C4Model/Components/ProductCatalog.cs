using Structurizr;

namespace TeeLab.C4Model.Components;

public class ProductCatalog
{
    private C4 Project { get; set; }
    private ContextDiagram Context { get; set; }
    private ContainerDiagram Container { get; set; }
    
    private Component Infrastructure { get; set; }
    private Component Application { get; set; }
    private Component Domain { get; set; }
    private Component Interfaces { get; set; }

    public ProductCatalog(ContextDiagram context, ContainerDiagram container, C4 project)
    {
        Project = project ?? throw new ArgumentNullException(nameof(project));
        Context = context ?? throw new ArgumentNullException(nameof(context));
        Container = container ?? throw new ArgumentNullException(nameof(container));
        
        Infrastructure = Container.ProductCatalog.AddComponent("Infrastructure", "Handles infrastructure and management.");
        Application = Container.ProductCatalog.AddComponent("Application", "Handles application and management.");
        Domain = Container.ProductCatalog.AddComponent("Domain", "Handles domain and management.");
        Interfaces = Container.ProductCatalog.AddComponent("Interfaces", "Handles interfaces and management.");
    }

    public void Generate()
    {
        Interfaces.Uses(Application, "Use");
        Application.Uses(Domain, "Use");
        Application.Uses(Infrastructure, "Use");
        
        Infrastructure.Uses(Context.Cloudinary, "Use");
        Infrastructure.Uses(Context.Supabase, "Use");
        
        ApplyStyles();
        Publish();
    }

    public void ApplyStyles()
    {
    }
    
    public void Publish()
    {
        ComponentView view = Project.ViewSet.CreateComponentView(Container.ProductCatalog, "Product Catalog Component", "");
        view.Title = "Product Catalog Component View";
        view.Add(Infrastructure);
        view.Add(Application);
        view.Add(Domain);
        view.Add(Interfaces);
        view.Add(Context.Supabase);
        
        view.Add(Context.Cloudinary);
    }
}