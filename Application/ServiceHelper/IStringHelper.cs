
namespace Application.ServiceHelper
{
    public interface IStringHelper
    {
        string SetCurrency(int price);

        //public string SetCurrency(int price)
        //{
        //    char[] ch = price.ToString().ToCharArray();

        //    int len = ch.Length;
        //    int counter = 0;

        //    List<char> newCh = new List<char>();

        //    for (int i = len - 1; i >= 0; i--)
        //    {
        //        counter += 1;
        //        if (counter == 3 && i != 0)
        //        {
        //            newCh.Add(ch[i]);
        //            newCh.Add('.');
        //            counter = 0;
        //        }
        //        else
        //        {
        //            newCh.Add(ch[i]);
        //        }

        //    }
        //    newCh.Reverse();

        //    string res = "Rp. " + string.Concat(newCh);

        //    return res;
        //}
    }
}
