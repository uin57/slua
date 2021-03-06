﻿// The MIT License (MIT)

// Copyright 2015 Siney/Pangweiwei siney@yeah.net
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using LuaInterface;
using System.Reflection;
using System.Runtime.InteropServices;

namespace SLua
{

    public partial class LuaObject
    {

        static protected LuaCSFunction lua_gc = new LuaCSFunction(luaGC);
        static protected LuaCSFunction lua_add = new LuaCSFunction(luaAdd);
        static protected LuaCSFunction lua_sub = new LuaCSFunction(luaSub);
        static protected LuaCSFunction lua_mul = new LuaCSFunction(luaMul);
        static protected LuaCSFunction lua_div = new LuaCSFunction(luaDiv);
        static protected LuaCSFunction lua_eq = new LuaCSFunction(luaEq);
        //static int objIndex = 0;
        //public static Dictionary<int, object> cache = new Dictionary<int, object>();
        static HashSet<string> exportname = new HashSet<string>();
        static Dictionary<string, string> namemap = new Dictionary<string, string>();

        static protected int newindex_ref = 0;
        static protected int index_ref = 0;

        public static void init(IntPtr l)
        {
            string newindexfun = @"

local getmetatable=getmetatable
local rawget=rawget
local error=error
local type=type
local function newindex(ud,k,v)
    local t=ud

    if type(ud)=='userdata' then
        local mt=getmetatable(ud)
        t=rawget(mt,'__type')
    end

    while t do
        local h=rawget(t,'set_'..k)
        if h then
            h(ud,v)
            return
        end
        t=rawget(t,'__parent')
    end
    error('can not find '..k)
end

return newindex

";

            string indexfun = @"

local type=type
local error=error
local rawget=rawget
local sub=string.sub
local function index(ud,k)

    local t=ud
    if type(ud)=='userdata' then
        local mt=getmetatable(ud)
        t=rawget(mt,'__type')
    end

    while t do
        local fun=rawget(t,k)
        if fun then return fun end
                
        fun=rawget(t,'get_'..k)
        if fun then return fun(ud) end
        t = rawget(t,'__parent')
    end
    if type(ud)=='userdata' then
        print(getmetatable(ud).__type.__typename)
    end
    error('Can not find '..k)
end

return index

";

            if (LuaDLL.luaL_dostring(l, newindexfun) != 0)
            {
                ThrowExceptionFromError(l);
                return;
            }
            newindex_ref = LuaDLL.luaL_ref(l, LuaIndexes.LUA_REGISTRYINDEX);

            if (LuaDLL.luaL_dostring(l, indexfun) != 0)
            {
                ThrowExceptionFromError(l);
                return;
            }
            index_ref = LuaDLL.luaL_ref(l, LuaIndexes.LUA_REGISTRYINDEX);


        }


        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static public int luaAdd(IntPtr l)
        {
            checkLuaObject(l, 1);

            LuaDLL.lua_getfield(l, -1, "op_Addition");

            if (LuaDLL.lua_isnil(l, -1))
            {
                LuaDLL.lua_pop(l, 2);
                LuaDLL.luaL_error(l, "No add operator");
                return 0;
            }
            LuaDLL.lua_remove(l, -2); // remove type table
            LuaDLL.lua_pushvalue(l, 1);
            LuaDLL.lua_pushvalue(l, 2);
            if (LuaDLL.lua_pcall(l, 2, 1, 0) != 0)
                ThrowExceptionFromError(l);
            return 1;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static public int luaSub(IntPtr l)
        {
            checkLuaObject(l, 1);

            LuaDLL.lua_getfield(l, -1, "op_Subtraction");

            if (LuaDLL.lua_isnil(l, -1))
            {
                LuaDLL.lua_pop(l, 2);
                LuaDLL.luaL_error(l, "No sub operator");
                return 0;
            }
            LuaDLL.lua_remove(l, -2); // remove type table
            LuaDLL.lua_pushvalue(l, 1);
            LuaDLL.lua_pushvalue(l, 2);
            if (LuaDLL.lua_pcall(l, 2, 1, 0) != 0)
                ThrowExceptionFromError(l);
            return 1;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static public int luaMul(IntPtr l)
        {
            checkLuaObject(l, 1);

            LuaDLL.lua_getfield(l, -1, "op_Multiply");

            if (LuaDLL.lua_isnil(l, -1))
            {
                LuaDLL.lua_pop(l, 2);
                LuaDLL.luaL_error(l, "No mul operator");
                return 0;
            }
            LuaDLL.lua_remove(l, -2); // remove type table
            LuaDLL.lua_pushvalue(l, 1);
            LuaDLL.lua_pushvalue(l, 2);
            if (LuaDLL.lua_pcall(l, 2, 1, 0) != 0)
                ThrowExceptionFromError(l);
            return 1;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static public int luaDiv(IntPtr l)
        {
            checkLuaObject(l, 1);

            LuaDLL.lua_getfield(l, -1, "op_Division");

            if (LuaDLL.lua_isnil(l, -1))
            {
                LuaDLL.lua_pop(l, 2);
                LuaDLL.luaL_error(l, "No div operator");
                return 0;
            }
            LuaDLL.lua_remove(l, -2); // remove type table
            LuaDLL.lua_pushvalue(l, 1);
            LuaDLL.lua_pushvalue(l, 2);
            if (LuaDLL.lua_pcall(l, 2, 1, 0) != 0)
                ThrowExceptionFromError(l);
            return 1;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static public int luaEq(IntPtr l)
        {
            checkLuaObject(l, 1);

            LuaDLL.lua_getfield(l, -1, "op_Equality");

            if (LuaDLL.lua_isnil(l, -1))
            {
                LuaDLL.lua_pop(l, 2);
                LuaDLL.luaL_error(l, "No eq operator");
                return 0;
            }
            LuaDLL.lua_remove(l, -2); // remove type table
            LuaDLL.lua_pushvalue(l, 1);
            LuaDLL.lua_pushvalue(l, 2);
            if (LuaDLL.lua_pcall(l, 2, 1, 0) != 0)
                ThrowExceptionFromError(l);
            return 1;
        }

        internal static void getTypeTable(IntPtr l, string t)
        {
            string[] subt = t.Split(new Char[] { '.' });

#if LUA_5_3
            LuaDLL.lua_pushglobaltable(l);
#else
            LuaDLL.lua_pushvalue(l, LuaIndexes.LUA_GLOBALSINDEX);
#endif
            
            for (int n = 0; n < subt.Length; n++)
            {
                t = subt[n];
                LuaDLL.lua_pushstring(l, t);
                LuaDLL.lua_rawget(l, -2);
                if (LuaDLL.lua_isnil(l, -1))
                {
                    LuaDLL.lua_pop(l, 1);
                    LuaDLL.lua_createtable(l, 0, 0);
                    LuaDLL.lua_pushstring(l, t);
                    LuaDLL.lua_pushvalue(l, -2);
                    LuaDLL.lua_rawset(l, -4);
                }
                LuaDLL.lua_remove(l, -2);
            }
        }

        internal static void createTypeMetatable(IntPtr l, Type self)
        {
            createTypeMetatable(l, self, null);
        }
        internal static void createTypeMetatable(IntPtr l, Type self, Type parent)
        {

            if (parent != null && parent != typeof(object))
            {
                LuaDLL.lua_pushstring(l, "__parent");
                getTypeTable(l, parent.FullName);
                LuaDLL.lua_rawset(l, -3);
            }

            LuaDLL.lua_pushstring(l, "__typename");
            LuaDLL.lua_pushstring(l, self.Name);
            LuaDLL.lua_rawset(l, -3);

            newTypeMeta(l);


            LuaDLL.lua_setfield(l, LuaIndexes.LUA_REGISTRYINDEX, self.AssemblyQualifiedName);
        }

        internal static void createTypeMetatable(IntPtr l, string name)
        {
            LuaDLL.lua_pushstring(l, "__typename");
            LuaDLL.lua_pushstring(l, name);
            LuaDLL.lua_rawset(l, -3);

            newTypeMeta(l);
            LuaDLL.lua_setfield(l, LuaIndexes.LUA_REGISTRYINDEX, name);
        }

        private static void newTypeMeta(IntPtr l)
        {
            LuaDLL.lua_newtable(l);

            LuaDLL.lua_getref(l, index_ref);
            LuaDLL.lua_setfield(l, -2, "__index");

            LuaDLL.lua_getref(l, newindex_ref);
            LuaDLL.lua_setfield(l, -2, "__newindex");

            LuaDLL.lua_pushstdcallcfunction(l, lua_add);
            LuaDLL.lua_setfield(l, -2, "__add");
            LuaDLL.lua_pushstdcallcfunction(l, lua_sub);
            LuaDLL.lua_setfield(l, -2, "__sub");
            LuaDLL.lua_pushstdcallcfunction(l, lua_mul);
            LuaDLL.lua_setfield(l, -2, "__mul");
            LuaDLL.lua_pushstdcallcfunction(l, lua_div);
            LuaDLL.lua_setfield(l, -2, "__div");
            LuaDLL.lua_pushstdcallcfunction(l, lua_eq);
            LuaDLL.lua_setfield(l, -2, "__eq");
            LuaDLL.lua_pushstdcallcfunction(l, lua_gc);
            LuaDLL.lua_setfield(l, -2, "__gc");

            // -2 is Type table
            LuaDLL.lua_pushvalue(l, -2);
            LuaDLL.lua_setfield(l, -2, "__type");
        }

        public static void addMember(IntPtr l, ref List<LuaCSFunction> list, LuaCSFunction func, string name)
        {
            list.Add(func);
            LuaDLL.lua_pushstdcallcfunction(l, func);
            LuaDLL.lua_setfield(l, -2, name);
        }

        public static void addMember(IntPtr l, LuaCSFunction func, string name)
        {
            LuaDLL.lua_pushstdcallcfunction(l, func);
            LuaDLL.lua_setfield(l, -2, name);
        }

        public static void addMember(IntPtr l, LuaCSFunction func)
        {
            LuaDLL.lua_pushstdcallcfunction(l, func);
            LuaDLL.lua_setfield(l, -2,func.Method.Name);
        }

        public static void newType(IntPtr l, ref List<LuaCSFunction> list, LuaCSFunction func)
        {
            list.Add(func);
            newType(l, func);
        }

        public static void newType(IntPtr l, LuaCSFunction func)
        {
            // mt
            LuaDLL.lua_newtable(l);
            LuaDLL.lua_pushstdcallcfunction(l, func);
            LuaDLL.lua_setfield(l, -2, "__call");

            LuaDLL.lua_getref(l, index_ref);
            LuaDLL.lua_setfield(l, -2, "__index");

            LuaDLL.lua_getref(l, newindex_ref);
            LuaDLL.lua_setfield(l, -2, "__newindex");

            LuaDLL.lua_setmetatable(l, -2);
        }

        static void ThrowExceptionFromError(IntPtr l)
        {
            string err = LuaDLL.lua_tostring(l, -1);
            LuaDLL.lua_pop(l, 1);

            throw new InvalidProgramException(err);
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static public int luaGC(IntPtr l)
        {
            int index = LuaDLL.luaS_rawnetobj(l, 1);
            ObjectCache t = ObjectCache.get(l);
            t.gc(index);
            return 0;
        }

        static public void checkLuaObject(IntPtr l, int p)
        {
            LuaDLL.luaL_checktype(l, p, LuaTypes.LUA_TUSERDATA);
            LuaDLL.lua_getmetatable(l, p);
            LuaDLL.lua_getfield(l, -1, "__type");
            if (LuaDLL.lua_isnil(l, -1))
            {
                LuaDLL.lua_pop(l, 1);
                LuaDLL.luaL_error(l, "expect luaobject as first argument");
            }
            LuaDLL.lua_remove(l, -2);
        }

        public static void pushObject(IntPtr l, object o)
        {
            ObjectCache oc = ObjectCache.get(l);
            oc.push(l, o);
        }

        public static int pushTry(IntPtr l)
        {
            LuaDLL.lua_pushstdcallcfunction(l, LuaState.errorReport);
            return LuaDLL.lua_gettop(l);
        }

        

        public static bool matchType(IntPtr l, int p, LuaTypes lt, Type t)
        {
            if (t == typeof(object))
            {
                return true;
            }
            switch (lt)
            {
                case LuaTypes.LUA_TNUMBER:
                    return t == typeof(Single) || t == typeof(double) || t == typeof(int) || t == typeof(Int16)
                        || t == typeof(UInt16) || t == typeof(UInt32) || t == typeof(byte) || t == typeof(Int64) || t.IsEnum;
                case LuaTypes.LUA_TUSERDATA:
                    return true;
                case LuaTypes.LUA_TSTRING:
                    return t.Name == "String";
                case LuaTypes.LUA_TBOOLEAN:
                    return t == typeof(bool);
                case LuaTypes.LUA_TTABLE:
                    return luaTypeCheck(l, p, t.Name);
                case LuaTypes.LUA_TFUNCTION:
                    return t == typeof(LuaFunction);
            }
            return false;
        }

        public static bool matchType(IntPtr l, int from, params Type[] types)
        {
            if (LuaDLL.lua_gettop(l) - from + 1 != types.Length)
                return false;

            for (int n = 0; n < types.Length; n++)
            {
                int p = n + from;
                LuaTypes t = LuaDLL.lua_type(l, p);
                if (!matchType(l, p, t, types[n]))
                    return false;
            }
            return true;
        }

        static public bool checkType(IntPtr l, int p, out float v)
        {
            v = (float)LuaDLL.luaL_checknumber(l, p);
            return true;
        }

        static public bool checkType(IntPtr l, int p, out float[] v)
        {
            LuaDLL.luaL_checktype(l, p, LuaTypes.LUA_TTABLE);
            int n = LuaDLL.lua_rawlen(l, p);
            v = new float[n];
            for (int k = 0; k < n; k++)
            {
                LuaDLL.lua_rawgeti(l, p, k + 1);
                float f;
                checkType(l, -1, out f);
                v[k] = f;
                LuaDLL.lua_pop(l, 1);
            }
            return true;
        }

        static public bool checkType(IntPtr l, int p, out string v)
        {
            //LuaDLL.luaL_checktype(l, p, LuaTypes.LUA_TSTRING);
            v = LuaDLL.lua_tostring(l, p);
            return true;
        }

        static public bool luaTypeCheck(IntPtr l, int p, string t)
        {
            if (LuaDLL.lua_type(l, p) != LuaTypes.LUA_TTABLE)
                return false;

            LuaDLL.lua_getfield(l, p, "__typename");
            LuaDLL.lua_pushstring(l, t);
            int equal = LuaDLL.lua_rawequal(l, -1, -2);
            LuaDLL.lua_pop(l, 2);

            if (equal == 0)
                return false;

            return true;
        }

        /*
        static public bool checkType(IntPtr l, int p, out Vector3 v)
        {
            v = Vector3.zero;
            if (!luaTypeCheck(l, p, "Vector3"))
                return false;

            LuaDLL.lua_getfield(l, p, "x");
            v.x = (float)LuaDLL.lua_tonumber(l, -1);
            LuaDLL.lua_getfield(l, p, "y");
            v.y = (float)LuaDLL.lua_tonumber(l, -1);
            LuaDLL.lua_getfield(l, p, "z");
            v.z = (float)LuaDLL.lua_tonumber(l, -1);
            LuaDLL.lua_pop(l, 3);
            return true;
        }

        static public bool checkType(IntPtr l, int p, out Vector2 v)
        {
            v = Vector3.zero;
            if (!luaTypeCheck(l, p, "Vector2"))
                return false;

            LuaDLL.lua_getfield(l, p, "x");
            v.x = (float)LuaDLL.lua_tonumber(l, -1);
            LuaDLL.lua_getfield(l, p, "y");
            v.y = (float)LuaDLL.lua_tonumber(l, -1);
            LuaDLL.lua_pop(l, 2);
            return true;
        }

        static public bool checkType(IntPtr l, int p, out Quaternion q)
        {
            q = Quaternion.identity;
            if (!luaTypeCheck(l, p, "Quaternion"))
                return false;

            LuaDLL.lua_getfield(l, p, "x");
            q.x = (float)LuaDLL.lua_tonumber(l, -1);
            LuaDLL.lua_getfield(l, p, "y");
            q.y = (float)LuaDLL.lua_tonumber(l, -1);
            LuaDLL.lua_getfield(l, p, "z");
            q.z = (float)LuaDLL.lua_tonumber(l, -1);
            LuaDLL.lua_getfield(l, p, "w");
            q.w = (float)LuaDLL.lua_tonumber(l, -1);
            LuaDLL.lua_pop(l, 4);
            return true;
        }
        */

        static public bool checkType(IntPtr l, int p, out int v)
        {
            v = (int) LuaDLL.luaL_checkinteger(l, p);
            return true;
        }

        static public bool checkType(IntPtr l, int p, out Int64 v)
        {
            v = LuaDLL.luaL_checkinteger(l, p);
            return true;
        }

        static public bool checkType(IntPtr l, int p, out bool v)
        {
            LuaDLL.luaL_checktype(l, p, LuaTypes.LUA_TBOOLEAN);
            v = LuaDLL.lua_toboolean(l, p);
            return true;
        }

        static public bool checkType(IntPtr l, int p, out LuaFunction f)
        {
            LuaDLL.luaL_checktype(l, p, LuaTypes.LUA_TFUNCTION);
            int fref = LuaDLL.luaL_ref(l, LuaIndexes.LUA_REGISTRYINDEX);
            f = new LuaFunction(l, fref);
            return true;
        }

        static public bool checkType(IntPtr l, int p, out UnityEngine.Events.UnityAction ua)
        {
            LuaDLL.luaL_checktype(l, p, LuaTypes.LUA_TFUNCTION);
            int r = LuaDLL.luaS_checkcallback(l, p);
            ua = () =>
            {
                LuaDLL.lua_pushstdcallcfunction(l, LuaState.errorReport);
                LuaDLL.lua_getref(l,r);
                if (LuaDLL.lua_pcall(l, 0, 0, -2) != 0)
                {
                    LuaDLL.lua_pop(l, 1);
                }
                LuaDLL.lua_pop(l, 1);
            };
            
            return true;
        }

        static public bool checkType<T>(IntPtr l, int p, out T o)
        {
            if (typeof(T).IsEnum)
            {
                LuaDLL.luaL_checktype(l, p, LuaTypes.LUA_TNUMBER);
                int i = LuaDLL.lua_tointeger(l, p);
                o = (T)Enum.ToObject(typeof(T), i);
                return true;
            }
            else
            {
                ObjectCache oc = ObjectCache.get(l);
                object obj = oc.get(l, p);
                o = (T)obj;
                return true;
            }
        }

        static public bool checkType(IntPtr l, int p, out object o)
        {
            LuaTypes t = LuaDLL.lua_type(l, p);
            switch (t)
            {
                case LuaTypes.LUA_TSTRING:
                    o = LuaDLL.lua_tostring(l, p);
                    return true;
                case LuaTypes.LUA_TFUNCTION:
                    o = LuaDLL.lua_tonumber(l, p);
                    return true;
                case LuaTypes.LUA_TUSERDATA:
                    ObjectCache oc = ObjectCache.get(l);
                    o = oc.get(l, p);
                    return true;
            }
            o = null;
            return false;
        }

        static public bool checkType(IntPtr l, int p, out object[] o)
        {
            throw new NotSupportedException();
        }

        static public bool checkType(IntPtr l, int p, out Type[] t)
        {
            throw new NotSupportedException();
        }

        static public bool checkType(IntPtr l, int p, out Array t)
        {
            throw new NotSupportedException();
        }

        static public bool checkType(IntPtr l, int p, out string[] t)
        {
            LuaDLL.luaL_checktype(l, p, LuaTypes.LUA_TTABLE);
            int n = LuaDLL.lua_rawlen(l, p);
            t = new string[n];
            for (int k = 0; k < n; k++)
            {
                LuaDLL.lua_rawgeti(l, p, k + 1);
                string f;
                checkType(l, -1, out f);
                t[k] = f;
                LuaDLL.lua_pop(l, 1);
            }
            return true;
        }

        public static void pushValue(IntPtr l, float o)
        {
            LuaDLL.lua_pushnumber(l, o);
        }

        public static void pushValue(IntPtr l, bool b)
        {
            LuaDLL.lua_pushboolean(l, b);
        }

        public static void pushValue(IntPtr l, string s)
        {
            LuaDLL.lua_pushstring(l, s);
        }

        public static void pushValue(IntPtr l, int i)
        {
            LuaDLL.lua_pushinteger(l, i);
        }

        public static void pushValue(IntPtr l, double d)
        {
            LuaDLL.lua_pushnumber(l, d);
        }

        public static void pushValue(IntPtr l, UnityEngine.Object o)
        {
            pushObject(l, o);
        }

        public static void pushValue(IntPtr l, object o)
        {
            Type t = o.GetType();
            if (t.IsEnum)
            {
                pushValue(l, (int)o);
            }
            else
            {
                pushObject(l, o);
            }
        }

        public static void pushValue(IntPtr l, object[] o)
        {
            LuaDLL.lua_newtable(l);
            for (int n = 0; n < o.Length; n++)
            {
                pushValue(l, o[n]);
                LuaDLL.lua_rawseti(l, -2, n + 1);
            }
        }

        public static void pushValue(IntPtr l, Quaternion o)
        {
            LuaDLL.lua_newtable(l);
            LuaDLL.lua_pushnumber(l, o.x);
            LuaDLL.lua_setfield(l, -2, "x");
            LuaDLL.lua_pushnumber(l, o.y);
            LuaDLL.lua_setfield(l, -2, "y");
            LuaDLL.lua_pushnumber(l, o.z);
            LuaDLL.lua_setfield(l, -2, "z");
            LuaDLL.lua_pushnumber(l, o.w);
            LuaDLL.lua_setfield(l, -2, "w");
            LuaDLL.lua_pushstring(l, "Quaternion");
            LuaDLL.lua_setfield(l, -2, "__typename");

            LuaDLL.lua_getglobal(l, "Quaternion_mt");
            LuaDLL.lua_setmetatable(l, -2);
        }

//        public static void pushValue(IntPtr l, Vector2 o)
//        {
//            LuaDLL.lua_newtable(l);
//            LuaDLL.lua_pushnumber(l, o.x);
//            LuaDLL.lua_setfield(l, -2, "x");
//            LuaDLL.lua_pushnumber(l, o.y);
//            LuaDLL.lua_setfield(l, -2, "y");
//            LuaDLL.lua_pushstring(l, "Vector2");
//            LuaDLL.lua_setfield(l, -2, "__typename");
//
//            LuaDLL.lua_getglobal(l, "Vector2_mt");
//            LuaDLL.lua_setmetatable(l, -2);
//        }
//
//        public static void pushValue(IntPtr l, Vector3 o)
//        {
//            LuaDLL.lua_newtable(l);
//            LuaDLL.lua_pushnumber(l, o.x);
//            LuaDLL.lua_setfield(l, -2, "x");
//            LuaDLL.lua_pushnumber(l, o.y);
//            LuaDLL.lua_setfield(l, -2, "y");
//            LuaDLL.lua_pushnumber(l, o.z);
//            LuaDLL.lua_setfield(l, -2, "z");
//            LuaDLL.lua_pushstring(l, "Vector3");
//            LuaDLL.lua_setfield(l, -2, "__typename");
//
//            LuaDLL.lua_getglobal(l, "Vector3_mt");
//            LuaDLL.lua_setmetatable(l, -2);
//        }
//
//        public static void pushValue(IntPtr l, Vector4 o)
//        {
//            LuaDLL.lua_newtable(l);
//            LuaDLL.lua_pushnumber(l, o.x);
//            LuaDLL.lua_setfield(l, -2, "x");
//            LuaDLL.lua_pushnumber(l, o.y);
//            LuaDLL.lua_setfield(l, -2, "y");
//            LuaDLL.lua_pushnumber(l, o.z);
//            LuaDLL.lua_setfield(l, -2, "z");
//            LuaDLL.lua_pushnumber(l, o.w);
//            LuaDLL.lua_setfield(l, -2, "w");
//            LuaDLL.lua_pushstring(l, "Vector4");
//            LuaDLL.lua_setfield(l, -2, "__typename");
//
//            LuaDLL.lua_getglobal(l, "Vector4_mt");
//            LuaDLL.lua_setmetatable(l, -2);
//        }


        public static T checkSelf<T>(IntPtr l)
        {
            ObjectCache t = ObjectCache.get(l);
            LuaDLL.luaL_checktype(l, 1, LuaTypes.LUA_TUSERDATA);
            object o = t.get(l, 1);
            if (o!=null)
            {
                return (T)o;
            }
            LuaDLL.luaL_error(l, "parameter 1 expect ud");
            return default(T);
        }

        public static void setBack(IntPtr l, object o)
        {
            ObjectCache t = ObjectCache.get(l);
            t.setBack(l, 1, o);
        }
    }
}