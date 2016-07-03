using Luna.UI.CRM;

namespace Luna.UI.Main
{
    internal interface IDocumentContainer : IDocumentHandler<TagListViewModel>,
                                    IDocumentHandler<DirectoryViewModel>,
                                    IDocumentHandler<ContactEditViewModel>,
                                    IDocumentHandler<ImportViewModel>
    {
    }
}