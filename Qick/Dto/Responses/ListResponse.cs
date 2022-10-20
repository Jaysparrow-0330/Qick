namespace Qick.Dto.Responses
{
    public class ListResponse<T>
    {
        public int TotalItem { get; set; }
        public IEnumerable<T>? Items { get; set; }

        public ListResponse(IEnumerable<T>? items)
        {
            if (items == null)
            {
                TotalItem = 0;
                Items = new List<T>();
            }
            else
            {
                TotalItem = items.ToList().Count();
                Items = items;
            }
        }
    }
}
