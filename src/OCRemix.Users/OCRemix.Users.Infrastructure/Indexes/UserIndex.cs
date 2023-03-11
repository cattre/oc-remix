// using System;
// using YesSql.Indexes;
// using OrchardCore.Users.Models;
//
// namespace OCRemix.Users.Infrastructure.Indexes;
//
// public class UserIndex : MapIndex
// {
//     public string Forename { get; set; }
//     public string Surname { get; set; }
//     public string FullName { get; set; }
//     public string Email { get; set; }
// }
//
// public class UserIndexProvider : IndexProvider<User>
// {
//     public override void Describe(DescribeContext<User> context)
//     {
//         context.For<UserIndex>()
//             .Map(user =>
//             {
//                 return
//                     new UserIndex
//                     {
//                         Forename = user.Forename,
//                         Surname = user.Surname,
//                         FullName = user.FullName,
//                         Email = user.Email
//                     };
//             });
//     }
// }
