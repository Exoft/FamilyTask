using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.ViewModel;

namespace WebClient.Abstractions
{
    public interface IMemberDataService
    {
        IEnumerable<MemberVm> Members { get; }
        MemberVm SelectedMember { get; }

        event EventHandler MembersChanged;
        event EventHandler SelectedMemberChanged;
        event EventHandler<string> UpdateMemberFailed;
        event EventHandler<string> CreateMemberFailed;


        Task UpdateMember(MemberVm model);
        Task CreateMember(MemberVm model);
        void SelectMember(Guid id);
        void SelectNullMember();

    }
}
