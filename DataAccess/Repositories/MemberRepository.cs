using BusinessObject;
using BusinessObject.Models;
using DataAccess.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class MemberRepository : IMemberRepository
    {
        private MemberDAO memberDAO = new MemberDAO();

        public Member checkLogin(string email, string password)
        {
            return memberDAO.checkLogin(email, password);  
        }

        public void createMember(Member m)
        {
           memberDAO.createMember(m);
        }

        public void deleteMember(int id)
        {
             memberDAO.deleteMember(id);
        }

        public List<Member> getMembers()
        {
           return memberDAO.getMembers();
        }

        public void updateMember(Member m)
        {
          memberDAO.updateMember(m);
        }
        public Member getMemberById(int id)
        {
           return memberDAO.getMemberById(id);
        }
    }
}
