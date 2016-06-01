using System;
using System.Reflection;

namespace Reflection
{
    class People
    {
        public string Name { get; set; }
        public int Age { get; set; }

        public People(string _name, int _age)
        {
            Name = _name;
            Age = _age;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Console.ReadLine();
            People p = new People("John", 32);
            Type type = p.GetType();
            PropertyInfo pinfo = type.GetProperty("Name");

            string s1 = p.Name;
            /*
            4.5
            C:\Work\tests\yaweiwfixit\Reflection\Program.cs @ 26:
009b04e9 8b4dec          mov     ecx,dword ptr [ebp-14h]
009b04ec 3909            cmp     dword ptr [ecx],ecx
009b04ee ff15644d3e00    call    dword ptr ds:[3E4D64h] (Reflection.People.get_Name(), mdToken: 06000001)
009b04f4 8945c4          mov     dword ptr [ebp-3Ch],eax
009b04f7 8b45c4          mov     eax,dword ptr [ebp-3Ch]
009b04fa 8945e0          mov     dword ptr [ebp-20h],eax

C:\Work\tests\yaweiwfixit\Reflection\Program.cs @ 27:
009b04fd 8b4de8          mov     ecx,dword ptr [ebp-18h]
009b0500 8b01            mov     eax,dword ptr [ecx]
009b0502 8b4028          mov     eax,dword ptr [eax+28h]
009b0505 ff5018          call    dword ptr [eax+18h]
009b0508 8945c0          mov     dword ptr [ebp-40h],eax
009b050b 8b45c0          mov     eax,dword ptr [ebp-40h]
009b050e 8945dc          mov     dword ptr [ebp-24h],eax

C:\Work\tests\yaweiwfixit\Reflection\Program.cs @ 28:
009b0511 8b4de4          mov     ecx,dword ptr [ebp-1Ch]
009b0514 8b01            mov     eax,dword ptr [ecx]
009b0516 8b4028          mov     eax,dword ptr [eax+28h]
009b0519 ff5018          call    dword ptr [eax+18h]
009b051c 8945bc          mov     dword ptr [ebp-44h],eax
009b051f 8b45bc          mov     eax,dword ptr [ebp-44h]
009b0522 8945d8          mov     dword ptr [ebp-28h],eax
            */
            string s2 = type.Name;
            string s3 = pinfo.Name;

            Console.WriteLine($"Name is: {p.Name}, Age is: {p.Age}");
            Console.ReadLine();
        }
    }
}
