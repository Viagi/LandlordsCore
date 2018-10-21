using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;

using ILRuntime.CLR.TypeSystem;
using ILRuntime.CLR.Method;
using ILRuntime.Runtime.Enviorment;
using ILRuntime.Runtime.Intepreter;
using ILRuntime.Runtime.Stack;
using ILRuntime.Reflection;
using ILRuntime.CLR.Utils;

namespace ILRuntime.Runtime.Generated
{
    unsafe class System_Collections_Generic_List_1_Google_Protobuf_Adapt_IMessage_Binding_Adaptor_Binding
    {
        public static void Register(ILRuntime.Runtime.Enviorment.AppDomain app)
        {
            BindingFlags flag = BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly;
            MethodBase method;
            Type[] args;
            Type type = typeof(System.Collections.Generic.List<Google.Protobuf.Adapt_IMessage.Adaptor>);
            args = new Type[]{typeof(System.Predicate<Google.Protobuf.Adapt_IMessage.Adaptor>)};
            method = type.GetMethod("FindIndex", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, FindIndex_0);

            args = new Type[]{typeof(System.Collections.Generic.IEnumerable<Google.Protobuf.Adapt_IMessage.Adaptor>)};
            method = type.GetConstructor(flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, Ctor_0);

        }


        static StackObject* FindIndex_0(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 2);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            System.Predicate<Google.Protobuf.Adapt_IMessage.Adaptor> @match = (System.Predicate<Google.Protobuf.Adapt_IMessage.Adaptor>)typeof(System.Predicate<Google.Protobuf.Adapt_IMessage.Adaptor>).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 2);
            System.Collections.Generic.List<Google.Protobuf.Adapt_IMessage.Adaptor> instance_of_this_method = (System.Collections.Generic.List<Google.Protobuf.Adapt_IMessage.Adaptor>)typeof(System.Collections.Generic.List<Google.Protobuf.Adapt_IMessage.Adaptor>).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            var result_of_this_method = instance_of_this_method.FindIndex(@match);

            __ret->ObjectType = ObjectTypes.Integer;
            __ret->Value = result_of_this_method;
            return __ret + 1;
        }


        static StackObject* Ctor_0(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 1);
            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            System.Collections.Generic.IEnumerable<Google.Protobuf.Adapt_IMessage.Adaptor> @collection = (System.Collections.Generic.IEnumerable<Google.Protobuf.Adapt_IMessage.Adaptor>)typeof(System.Collections.Generic.IEnumerable<Google.Protobuf.Adapt_IMessage.Adaptor>).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);


            var result_of_this_method = new System.Collections.Generic.List<Google.Protobuf.Adapt_IMessage.Adaptor>(@collection);

            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }


    }
}
