using Luna.UI.CRM;

namespace Luna.UI.Main
{
    internal interface IDocumentContainer : IDocumentHandler<TagEditViewModel>,
                                    IDocumentHandler<DirectoryViewModel>,
                                    IDocumentHandler<ContactEditViewModel>,
                                    IDocumentHandler<ImportViewModel>
    {
    }
}