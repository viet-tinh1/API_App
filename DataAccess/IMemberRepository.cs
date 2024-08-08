using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject
{
    internal interface IMemberRepository
    {
        void deleteMember(int id);
        void updateMember(Member m);
        List<Member> getMembers();
        Member checkLogin(string email, string password);
        public void createMember(Member m);
        public Member getMemberById(int id);
    }
}
