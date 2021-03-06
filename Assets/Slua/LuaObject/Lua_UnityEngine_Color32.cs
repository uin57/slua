﻿using UnityEngine;
using System;
using LuaInterface;
using SLua;
using System.Collections.Generic;
public class Lua_UnityEngine_Color32 : LuaObject {
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int constructor(IntPtr l) {
		LuaDLL.lua_remove(l,1);
		UnityEngine.Color32 o;
		if(matchType(l,1,typeof(System.Byte),typeof(System.Byte),typeof(System.Byte),typeof(System.Byte))){
			System.Byte a1;
			checkType(l,1,out a1);
			System.Byte a2;
			checkType(l,2,out a2);
			System.Byte a3;
			checkType(l,3,out a3);
			System.Byte a4;
			checkType(l,4,out a4);
			o=new UnityEngine.Color32(a1,a2,a3,a4);
			pushObject(l,o);
			return 1;
		}
		return 0;
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int Lerp(IntPtr l) {
		try{
			UnityEngine.Color32 a1;
			checkType(l,1,out a1);
			UnityEngine.Color32 a2;
			checkType(l,2,out a2);
			System.Single a3;
			checkType(l,3,out a3);
			UnityEngine.Color32 ret=UnityEngine.Color32.Lerp(a1,a2,a3);
			pushValue(l,ret);
			return 1;
		}
		catch(Exception e) {
			LuaDLL.luaL_error(l, e.ToString());
			return 0;
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int get_r(IntPtr l) {
		UnityEngine.Color32 o = checkSelf<UnityEngine.Color32>(l);
		pushValue(l,o.r);
		return 1;
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int set_r(IntPtr l) {
		UnityEngine.Color32 o = checkSelf<UnityEngine.Color32>(l);
		System.Byte v;
		checkType(l,2,out v);
		o.r=v;
		setBack(l,o);
		return 0;
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int get_g(IntPtr l) {
		UnityEngine.Color32 o = checkSelf<UnityEngine.Color32>(l);
		pushValue(l,o.g);
		return 1;
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int set_g(IntPtr l) {
		UnityEngine.Color32 o = checkSelf<UnityEngine.Color32>(l);
		System.Byte v;
		checkType(l,2,out v);
		o.g=v;
		setBack(l,o);
		return 0;
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int get_b(IntPtr l) {
		UnityEngine.Color32 o = checkSelf<UnityEngine.Color32>(l);
		pushValue(l,o.b);
		return 1;
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int set_b(IntPtr l) {
		UnityEngine.Color32 o = checkSelf<UnityEngine.Color32>(l);
		System.Byte v;
		checkType(l,2,out v);
		o.b=v;
		setBack(l,o);
		return 0;
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int get_a(IntPtr l) {
		UnityEngine.Color32 o = checkSelf<UnityEngine.Color32>(l);
		pushValue(l,o.a);
		return 1;
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int set_a(IntPtr l) {
		UnityEngine.Color32 o = checkSelf<UnityEngine.Color32>(l);
		System.Byte v;
		checkType(l,2,out v);
		o.a=v;
		setBack(l,o);
		return 0;
	}
	static public void reg(IntPtr l) {
		getTypeTable(l,"UnityEngine.Color32");
		addMember(l,Lerp, "Lerp");
		addMember(l,get_r, "get_r");
		addMember(l,set_r, "set_r");
		addMember(l,get_g, "get_g");
		addMember(l,set_g, "set_g");
		addMember(l,get_b, "get_b");
		addMember(l,set_b, "set_b");
		addMember(l,get_a, "get_a");
		addMember(l,set_a, "set_a");
		newType(l, constructor);
		createTypeMetatable(l, typeof(UnityEngine.Color32));
		LuaDLL.lua_pop(l, 1);
	}
}
