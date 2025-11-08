using Domain.Model;
using Application.Service;
using Domain.DataContext;

namespace Infrastructure.Repository
{
    public class MasterRefRepository : GenericRepository<MasterRef>, IMasterRefRepository
    {
        private readonly DataContext db;
        public MasterRefRepository(DataContext _db) : base(_db)
        {
            this.db = _db;
        }
        public MasterRef GetRef(string refName)
        {
            var query = from q in db.MasterRef
                        where q.RefName == refName
                        select q;

            DateTime nowDate = DateTime.Now.Date;
            string date = nowDate.ToString("yyyyMMdd");
            string code = query.FirstOrDefault().RefCode;
            //ORD-20251028-001
            string value = query.FirstOrDefault().RefValue;
            DateTime getLastRefDate = query.FirstOrDefault().RefLastDate == null ? nowDate : query.FirstOrDefault().RefLastDate.Value.Date;
            if (getLastRefDate == null)
            {
                getLastRefDate = nowDate;
            }
            int getLastRefValue = query.FirstOrDefault().RefLastCounter == null ? 0 : query.FirstOrDefault().RefLastCounter.Value;
            if (getLastRefValue == null)
            {
                getLastRefValue = 0;
            }else
            {
                if (getLastRefDate != nowDate)
                {
                    getLastRefValue = 0;
                }
            }
            string[] strValue = value.Split('-');
            string newRes = "";
            string val = "";
            foreach (var item in strValue)
            {
                if (item == "[code]")
                {
                    newRes += code;
                    newRes += "-";
                }
                else if (item == "[date]")
                {
                    newRes += date;
                    newRes += "-";
                }
                else if (item == "[value]")
                {
                    if (getLastRefDate == nowDate)
                    {
                        getLastRefValue += 1;
                        if (getLastRefValue < 10)
                        {
                            val = "00" + getLastRefValue.ToString();
                        }
                        else if (getLastRefValue < 100)
                        {
                            val = "0" + getLastRefValue.ToString();
                        }
                        else if (getLastRefValue < 1000)
                        {
                            val = getLastRefValue.ToString();
                        }

                        newRes += val;
                    }
                    else if (getLastRefDate != nowDate)
                    {
                        getLastRefValue = 1;
                        if (getLastRefValue < 10)
                        {
                            val = "00" + getLastRefValue.ToString();
                        }
                        else if (getLastRefValue < 100)
                        {
                            val = "0" + getLastRefValue.ToString();
                        }
                        else if (getLastRefValue < 1000)
                        {
                            val = getLastRefValue.ToString();
                        }
                        newRes += val;
                    }

                }
            }

            MasterRef mr = new MasterRef();
            var rf = db.MasterRef.Find(query.FirstOrDefault().Id);
            mr = rf;
            mr.RefLastCounter = getLastRefValue;
            mr.RefLastDate = nowDate;
            mr.RefLastValue = newRes;
            //var rf = db.MasterRef.Find(query.FirstOrDefault().Id);
            //if (rf != null)
            //{
            //    rf.RefLastCounter = getLastRefValue;
            //    rf.RefLastDate = getLastRefDate;
            //    rf.RefLastValue = newRes;
            //    db.MasterRef.Update(rf);
            //    db.SaveChanges();
            //}

            return mr;

        }
    }
}
