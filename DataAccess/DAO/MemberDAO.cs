using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public class MemberDAO
    {
        readonly PRN231_Assignment1Context _context = new PRN231_Assignment1Context();

        public MemberDAO()
        {
        }

        public MemberDAO(PRN231_Assignment1Context context)
        {
            _context = context;
        }
        public void deleteMember(int id)
        {
            var m = _context.Members.FirstOrDefault(x => x.MemberId == id);
            if (m != null)
            {
                _context.Members.Remove(m);
                _context.SaveChanges();
            }
        }

        public List<Member> getMembers()
        {
            return _context.Members.ToList();
        }

        public void updateMember(Member m)
        {
           var member = _context.Members.FirstOrDefault(x => x.MemberId == m.MemberId);
            if (member != null) { 
                member.Email = m.Email;
                member.City = m.City;
                member.Country = m.Country;
                member.Password = m.Password;
                member.CompanyName = m.CompanyName;
                
                _context.Members.Update(member);
                _context.SaveChanges();
            }
        }
        public Member checkLogin(string email, string password) {
                Member member = _context.Members.FirstOrDefault(x => x.Email.Equals(email) && x.Password.Equals(password));
                return member;
           
        }
        public void createMember(Member m) {
            _context.Members.Add(m);
            _context.SaveChanges();
        }
        public Member getMemberById(int id) {
            return _context.Members.FirstOrDefault(x => x.MemberId == id);
        }
    }
    
}
