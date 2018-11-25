using _1DV607A2.Model;
using System.Collections.Generic;

namespace _1DV607A2.View
{
    interface IUserInterface
    {
        void Draw();
        void Update();

        void ShowMainMenuUser();
        void ShowMainMenuGuest();

        void ShowMembers(ListDisplayMode displayMode, IEnumerable<MemberData> members);
        void ShowBoats(ListDisplayMode displayMode, IEnumerable<BoatData> boats);
        void ShowMember(MemberData member);
        void ShowBoat(BoatData boat);
        void ShowMemberEditor(DataMode dataMode, MemberData member);
        void ShowBoatEditor(DataMode dataMode, BoatData boat);
    }
}
