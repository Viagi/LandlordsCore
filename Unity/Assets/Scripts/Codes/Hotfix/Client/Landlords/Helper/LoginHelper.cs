using ET.Landlords;
using System;
using System.Net;
using System.Net.Sockets;

namespace ET.Client
{
    namespace Landlords
    {
        public static class LoginHelper
        {
            public static async ETTask<int> Login(Scene clientScene, string account, string password)
            {
                try
                {
                    // 创建一个ETModel层的Session
                    clientScene.RemoveComponent<RouterAddressComponent>();
                    // 获取路由跟realmDispatcher地址
                    RouterAddressComponent routerAddressComponent = clientScene.GetComponent<RouterAddressComponent>();
                    if (routerAddressComponent == null)
                    {
                        routerAddressComponent = clientScene.AddComponent<RouterAddressComponent, string, int>(ConstValue.RouterHttpHost, ConstValue.RouterHttpPort);
                        await routerAddressComponent.Init();

                        clientScene.AddComponent<NetClientComponent, AddressFamily>(routerAddressComponent.RouterManagerIPAddress.AddressFamily);
                    }
                    IPEndPoint realmAddress = routerAddressComponent.GetRealmAddress(account);

                    ET.Landlords.R2C_Login r2CLogin;
                    using (Session session = await RouterHelper.CreateRouterSession(clientScene, realmAddress))
                    {
                        r2CLogin = (ET.Landlords.R2C_Login)await session.Call(new ET.Landlords.C2R_Login() { Account = account, Password = password });
                    }

                    //登录失败
                    if (r2CLogin.Error > 0)
                    {
                        clientScene.RemoveComponent<NetClientComponent>();
                        return r2CLogin.Error;
                    }

                    // 创建一个gate Session,并且保存到SessionComponent中
                    Session gateSession = await RouterHelper.CreateRouterSession(clientScene, NetworkHelper.ToIPEndPoint(r2CLogin.Address));
                    clientScene.AddComponent<SessionComponent>().Session = gateSession;

                    ET.Landlords.G2C_LoginGate g2CLoginGate = (ET.Landlords.G2C_LoginGate)await gateSession.Call(
                        new ET.Landlords.C2G_LoginGate() { Key = r2CLogin.Key });

                    Log.Debug("登陆gate成功!");
                    return 0;
                }
                catch (Exception e)
                {
                    Log.Error(e);
                    return ErrorCode.ERR_MyException;
                }
            }

            public static async ETTask<int> Register(Scene clientScene, string account, string password)
            {
                try
                {
                    // 创建一个ETModel层的Session
                    clientScene.RemoveComponent<RouterAddressComponent>();
                    // 获取路由跟realmDispatcher地址
                    RouterAddressComponent routerAddressComponent = clientScene.GetComponent<RouterAddressComponent>();
                    if (routerAddressComponent == null)
                    {
                        routerAddressComponent = clientScene.AddComponent<RouterAddressComponent, string, int>(ConstValue.RouterHttpHost, ConstValue.RouterHttpPort);
                        await routerAddressComponent.Init();

                        clientScene.AddComponent<NetClientComponent, AddressFamily>(routerAddressComponent.RouterManagerIPAddress.AddressFamily);
                    }
                    IPEndPoint realmAddress = routerAddressComponent.GetRealmAddress(account);

                    R2C_Register r2C_Register;
                    using (Session session = await RouterHelper.CreateRouterSession(clientScene, realmAddress))
                    {
                        r2C_Register = (R2C_Register)await session.Call(new C2R_Register() { Account = account, Password = password });
                    }

                    //注册失败
                    if (r2C_Register.Error > 0)
                    {
                        clientScene.RemoveComponent<NetClientComponent>();
                        return r2C_Register.Error;
                    }

                    // 创建一个gate Session,并且保存到SessionComponent中
                    Session gateSession = await RouterHelper.CreateRouterSession(clientScene, NetworkHelper.ToIPEndPoint(r2C_Register.Address));
                    clientScene.AddComponent<SessionComponent>().Session = gateSession;

                    ET.Landlords.G2C_LoginGate g2CLoginGate = (ET.Landlords.G2C_LoginGate)await gateSession.Call(
                        new ET.Landlords.C2G_LoginGate() { Key = r2C_Register.Key });

                    Log.Debug("登陆gate成功!");
                    return 0;
                }
                catch (Exception e)
                {
                    Log.Error(e);
                    return ErrorCode.ERR_MyException;
                }
            }

            public static async ETTask<int> Connect(Scene clientScene, string address, Entity unit)
            {
                try
                {
                    // 创建一个ETModel层的Session
                    clientScene.RemoveComponent<RouterAddressComponent>();
                    // 获取路由跟realmDispatcher地址
                    RouterAddressComponent routerAddressComponent = clientScene.GetComponent<RouterAddressComponent>();
                    if (routerAddressComponent == null)
                    {
                        routerAddressComponent = clientScene.AddComponent<RouterAddressComponent, string, int>(ConstValue.RouterHttpHost, ConstValue.RouterHttpPort);
                        await routerAddressComponent.Init();

                        clientScene.AddComponent<NetClientComponent, AddressFamily>(routerAddressComponent.RouterManagerIPAddress.AddressFamily);
                    }

                    // 创建一个gate Session,并且保存到SessionComponent中
                    Session gateSession = await RouterHelper.CreateRouterSession(clientScene, NetworkHelper.ToIPEndPoint(address));
                    clientScene.AddComponent<SessionComponent>().Session = gateSession;

                    G2C_ConnectGate g2C_ConnectGate = (G2C_ConnectGate)await gateSession.Call(
                        new C2G_ConnectGate() { UnitId = unit.InstanceId, ActorId = unit.DomainScene().InstanceId });

                    Log.Debug("登陆gate成功!");
                    return 0;
                }
                catch (Exception e)
                {
                    Log.Error(e);
                    return ErrorCode.ERR_MyException;
                }
            }
        }
    }
}