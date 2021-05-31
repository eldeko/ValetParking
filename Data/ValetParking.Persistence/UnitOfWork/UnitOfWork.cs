namespace ValetParking.Persistence.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private ValetParkingDbContext context;

        public ValetParkingDbContext Context
        {
            get
            {
                if (context == null)
                {
                    context = new ValetParkingDbContext();

                }
                return context;
            }
        }

        public int Commit()
        {

            return context.SaveChanges();
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }

}
