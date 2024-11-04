using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*编写一个控制台应用程序
1、登录界面，让用户通过登录不同的账号，进入不同的界面。
管理者：admin=“manager”，password="manager123"；
购物者：admin="shopper"，password="shopper123"；
并且当账号密码输入错误时，需进行提示，并让用户重新输入。
2、管理者进入管理界面添加商品，每个商品信息包括但不限于名称、价格、数量。
添加商品时需注意名称是否重复（是否已添加，如果已经添加过了，就让数量叠加，不需要创建一个同名的新商品），数量、价格的输入格式限制，以防止程序被挂起。
单个商品添加完成之后，可以选择回到登录界面或者继续添加商品。
3、购物者进入购物界面时，要将现有的商品的信息罗列出来，让购物者可以进行选择购买。
购买商品时需注意名称是否重复（是否已购买，如果已经购买过了，就让数量叠加，不需要创建一个同名的新商品），数量、价格的输入格式限制，以防止程序被挂起。
单个商品购买完成之后，打印本次消费以及总消费。并且可以让用户选择回到登录界面或者继续购买商品。*/

namespace jxufeCPlusHomework02
{
    internal class Program
    {

        static List<Shop> shopList = new List<Shop>();//存放所有的商品
        static List<Shop> buyList = new List<Shop>();//存放所有的商品
        static void Main(string[] args)
        {
            Shop shop1 = new Shop();//new 关键字 进行实例化
            shop1.name = "苹果";
            shop1.price = 1.2;
            shop1.number = 100;
            shopList.Add(shop1);
            Shop shop2 = new Shop();
            shop2.name = "西瓜";
            shop2.price = 1.5;
            shop2.number = 88;
            shopList.Add(shop2);
            Login();
        }

        private static void Buy()
        {
            if (buyList.Count > 0)
            {
                Console.WriteLine("当前购物车有：");
                for (int i = 0; i < buyList.Count; i++)
                {
                    Console.WriteLine($"{i + 1}、{buyList[i].name}，数量为{buyList[i].number}，价格为{buyList[i].price}");
                }
            }

            Console.WriteLine("请输入要购买的商品名称:");
            string name = Console.ReadLine();
            //判断输入的商品是否存在
            bool b = false;
            int index = 0;
            while (!b)
            {
                for (int i = 0; i < shopList.Count; i++)
                {
                    if (shopList[i].name == name)
                    {
                        b = true;
                        index = i;
                    }
                }
                if (!b)
                {
                    //不存在，告诉不存在，让重新输出
                    Console.WriteLine("输入的商品不存在，请重新输入");
                    name = Console.ReadLine();
                    //重新输入 就要重新判断
                }
            }
            //存在，可以购买
            Console.WriteLine("请输入购买的数量:");
            double number = Function();//显示转换，有可能会出问题

            //提示数量输入不对，让重新输入
            bool c = false;
            while (!c)
            {
                if (shopList[index].number >= number && number > 0)
                {
                    c = true;
                }
                if (!c)
                {
                    Console.Write("输入的数量不对，请重新输入:");
                    number = Function();
                }
            }

            //判断是不是已经买过了？  商品第一次买，就正常买，如果是已经买过了，就让数量叠加
            bool d = false;

            for (int i = 0; i < buyList.Count; i++)
            {
                if (buyList[i].name == name)
                {
                    Shop buyShop = buyList[i];
                    buyShop.number += number;
                    buyList[i] = buyShop;
                    d = true;
                }
            }

            if (!d)
            {
                Shop shop = new Shop();
                shop.name = shopList[index].name;
                shop.price = shopList[index].price;
                shop.number = number;
                buyList.Add(shop);
            }


            Console.WriteLine("添加购物车成功");

            //100苹果，买走了20个，下一次购买，又买了50个苹果
            //100个被买走了50个，刷新一下库存

            Shop shopListShop = shopList[index];
            shopListShop.number -= number;
            shopList[index] = shopListShop;

            //算总账
            Console.WriteLine("继续购买请输入1，结束购买请输入2, 重新登录请输入3");
            string option = Console.ReadLine();
            b = false;
            while (!b)
            {
                switch (option)
                {
                    case "1":
                        b = true;
                        Buy();
                        break;
                    case "2":
                        b = true;
                        double sum = 0;
                        for (int i = 0; i < buyList.Count; i++)
                        {
                            sum += buyList[i].price * buyList[i].number;
                        }
                        Console.WriteLine("您购买了:");
                        for (int i = 0; i < buyList.Count; i++)
                        {
                            Console.WriteLine($"{i + 1}、{buyList[i].name}，数量为{buyList[i].number}，价格为{buyList[i].price}");
                        }
                        Console.WriteLine($"总价为{Math.Round(sum, 2)}");
                        break;
                    case "3":
                        Login();
                        break;
                    default:
                        Console.WriteLine("指令输入错误，请重新输入");
                        option = Console.ReadLine();
                        break;
                }
            }
        }


        private static void AddShop()
        {
            Console.WriteLine("请输入商品名称:");
            string name = Console.ReadLine();
            Console.WriteLine("请输入商品价格:");
            double price = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("请输入商品数量:");
            int number = Convert.ToInt32(Console.ReadLine());
            Shop shop = new Shop();
            shop.name = name;
            shop.price = price;
            shop.number = number;
            bool b = false;
            for (int i = 0; i < shopList.Count; i++)
            {
                if (shopList[i].name == name)
                {
                    shop = shopList[i];
                    shop.number += number;
                    shop.price = price;
                    shopList[i] = shop;
                    b = true;
                }
            }
            if (!b)
            {
                shopList.Add(shop);
            }

            Console.Write("目前有:");
            Console.WriteLine();
            for (int i = 0; i < shopList.Count; i++)
            {
                Console.WriteLine($"{i + 1}、{shopList[i].name}，数量为{shopList[i].number}，价格为{shopList[i].price}");
            }

            Console.WriteLine("继续添加请输入1，重新登录请输入2，退出请输入3");
            string option = Console.ReadLine();
            b = false;
            while (!b)
            {
                switch (option)
                {
                    case "1":
                        b = true;
                        AddShop();
                        break;
                    case "2":
                        Login();
                        break;
                    case "3":
                        break;
                    default:
                        Console.WriteLine("指令输入错误，请重新输入");
                        option = Console.ReadLine();
                        break;
                }
            }
        }

        private static void Login()
        {
            Console.WriteLine("请输入账号:");
            string account = Console.ReadLine();
            Console.WriteLine("请输入密码:");
            string password = Console.ReadLine();
            if (account == "manager" && password == "manager123")
            {
                Console.WriteLine("欢迎管理员");
                Console.Write("目前有:");
                Console.WriteLine();
                for (int i = 0; i < shopList.Count; i++)
                {
                    Console.WriteLine($"{i + 1}、{shopList[i].name}，数量为{shopList[i].number}，价格为{shopList[i].price}");
                }
                Console.WriteLine("添加商品请输入1，重新登录请输入2，退出请输入3");
                int choice = Convert.ToInt32(Console.ReadLine());
                if (choice == 1)
                {
                    AddShop();
                }
                else if (choice == 2)
                {
                    Login();
                }
                else if (choice == 3)
                {
                    Console.WriteLine("退出成功");
                }
            }
            else if (account == "shopper" && password == "shopper123")
            {
                Console.Write("欢迎光临，目前有:");
                Console.WriteLine();
                for (int i = 0; i < shopList.Count; i++)
                {
                    Console.WriteLine($"{i + 1}、{shopList[i].name}，数量为{shopList[i].number}，价格为{shopList[i].price}");
                }

                Console.WriteLine("购买商品请输入1，重新登录请输入2，退出请输入3");
                int choice = Convert.ToInt32(Console.ReadLine());
                if (choice == 1)
                {
                    Buy();
                }
                else if (choice == 2)
                {
                    Login();
                }
                else if (choice == 3)
                {
                    Console.WriteLine("退出成功");
                }
            }
            else
            {
                Console.WriteLine("账号或密码错误，请重新输入");
                Login();
            }
        }

        /// <summary>
        /// 获得一个指定格式的字符串返回的double类型
        /// </summary>
        static double Function()
        {
            string s = Console.ReadLine();//1.4.53.4.153 除了最多有一个小数点（0个或1个小数点）（小数点不能用作开头和结尾），其他的是纯数字
            //判断 如果s是一个可以转换成 double类型的 字符串，就可以返回回去
            //如果s 强转成 double类型 会报错，那么就提示 格式错误，让重新输入
            bool b = true;
            int sum = 0;
            do
            {
                b = true;
                for (int i = 0; i < s.Length; i++)
                {
                    if (s[0] == '.' || s[s.Length - 1] == '.')//判断小数点是否用作了开头的结尾
                    {
                        b = false;
                        //不是
                        break;
                    }
                    if (s[i] == '.')
                    {
                        sum++;//判断小数点的数量
                        if (sum > 1)
                        {
                            b = false;
                            //不是
                            break;
                        }
                    }
                }
                if (b)
                {
                    for (int i = 0; i < s.Length; i++)
                    {
                        if ((s[i] >= '0' && s[i] <= '9') || s[i] == '.')
                        {

                        }
                        else
                        {
                            b = false;
                            break;
                        }
                    }
                }
                if (b)
                {
                    //是满足格式的
                }
                else
                {
                    //格式不对，重新输入
                    Console.WriteLine("输入的格式错误，请重新输入");
                    s = Console.ReadLine();
                }
            } while (!b);
            return double.Parse(s);
        }

        struct Shop
        {
            public string name;//名字
            public double price;//价格
            public double number;//数量
        }

    }
}
