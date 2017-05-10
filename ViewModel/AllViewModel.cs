using Kurs.Models;

namespace Kurs.ViewModel
{
    public class AllViewModel
    {
        public User user { get; set; }
        public Login login { get; set; }
        public UsersGPX gpx { get; set; }
        public Password pass { get; set; }
    }
}
