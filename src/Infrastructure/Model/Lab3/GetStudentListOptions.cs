using Infrastructure.Enum;

namespace Infrastructure.Model.Lab3
{
    public class GetStudentListOptions
    {
        public int Limit { get; set; }
        public bool SortByName { get; set; }
        public bool ThrowException { get; set; }
        public int Offset { get; set; }
        public string Like { get; set; }
        public string Columns { get; set; }
        public string GlobalLike { get; set; }

        public ResultContentType ContentType { get; set; }

    }
}
