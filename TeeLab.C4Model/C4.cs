using Structurizr;
using Structurizr.Api;
using TeeLab.C4Model.Components;

namespace TeeLab.C4Model;

public class C4
{
    private string ApiKey { set; get; }
    private string ApiSecret { set; get; }
    private long WorkspaceId { set; get; }
    private string WorkspaceName { set; get; }
    private string WorkspaceDescription { set; get; }

    public StructurizrClient Project { get; set; }
    public Workspace Workspace { get; set; }
    public Model Model { get; set; }
    public ViewSet ViewSet { get; set; }
    
    public C4()
    {
        ApiKey = "a0152e34-512b-4265-8932-53575ce5a8dd";
        ApiSecret = "60ac86dd-6aa7-4e32-bf0e-a7cf05145f44";
        WorkspaceId = 101371;
        WorkspaceName = "TeeLab - Web Applications";
        WorkspaceDescription = "TeeLab - C4 Model";

        Project = new StructurizrClient(ApiKey, ApiSecret);
        Workspace = new Workspace(WorkspaceName, WorkspaceDescription);
        Model = Workspace.Model;
        ViewSet = Workspace.Views;
    }

    public void Generate()
    {
        var context = new ContextDiagram(this);
        context.Generate();
        var container = new ContainerDiagram(context, this);
        container.Generate();
        
        var api = new ApiComponent(context, container, this);
        api.Generate();
        var orderProcessing = new OrderProcessing(context, container, this);
        orderProcessing.Generate();
        var designStudio = new DesignStudio(context, container, this);
        designStudio.Generate();
        var paymentGateway = new PaymentGateway(context, container, this);
        paymentGateway.Generate();
        var orderFulfillment = new OrderFulfillment(context, container, this);
        orderFulfillment.Generate();
        var productCatalog = new ProductCatalog(context, container, this);
        productCatalog.Generate();
        var userManagement = new UserManagement(context, container, this);
        userManagement.Generate();
        
        Project.PutWorkspace(WorkspaceId, Workspace);
    }
    
}