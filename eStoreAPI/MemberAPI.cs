using BusinessObject.Models;
using DataAccess.Repositories;
using eStoreAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;
namespace eStoreAPI
{
    [Route("api/member")]
    [ApiController]
    public class MemberAPI : ControllerBase
    {
        private MemberRepository memberRepository = new MemberRepository();
        [HttpGet("getMember")]
        public ActionResult<IEnumerable<Member>> GetProducts()
        {
            return memberRepository.getMembers();
        }
        [HttpPost("updateMember")]
        public IActionResult updateProduct(Member m)
        {
            Member member = new Member()
            {
                Email = m.Email,
                City = m.City,
                Country = m.Country,
                Password = m.Password,
                CompanyName = m.CompanyName,
                MemberId = m.MemberId

            };
            memberRepository.updateMember(member);
            return NoContent();
        }
        [HttpGet("deleteMember")]
        public IActionResult deleteMember(int memberid)
        {
            memberRepository.deleteMember(memberid);
            return NoContent();
        }

        [HttpPost("checkLogin")]
        public ActionResult<Member> checkLogin([FromBody] LoginModel loginModel)
        {
            return memberRepository.checkLogin(loginModel.Email, loginModel.Password);
        }
        [HttpPost("createMember")]
        public IActionResult createMember(Member m)
        {
            Member member = new Member()
            {
                MemberId = 0,
                Email = m.Email,
                City = m.City,
                Country = m.Country,
                Password = m.Password,
                CompanyName = m.CompanyName
            };
            memberRepository.createMember(member);
            return NoContent();
        }
        [HttpGet("getMemberById")]
        public ActionResult<Member> getMemberById(int id)
        {
            return memberRepository.getMemberById(id);
        }
    }
}
