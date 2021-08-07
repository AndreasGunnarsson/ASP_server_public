using Core.Entities;

namespace Core.Interfaces
{
    public interface IIOArticle
    {
        void Write();
        void Read();
    }
}

// Ska implementeras i Infrastructure.
// Input/output for disk reading/writing of an article to the wwwroot-folder.
// TODO: Måste bestämma något filformat. Markdown?
// TODO: Måste bestämma en retur-typ.
