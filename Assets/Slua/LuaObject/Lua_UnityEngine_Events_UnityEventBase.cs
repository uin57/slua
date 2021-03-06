﻿using UnityEngine;
using System;
using LuaInterface;
using SLua;
using System.Collections.Generic;
public class Lua_UnityEngine_Events_UnityEventBase : LuaObject {
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int constructor(IntPtr l) {
		return 0;
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int GetPersistentEventCount(IntPtr l) {
		try{
			UnityEngine.Events.UnityEventBase self=checkSelf<UnityEngine.Events.UnityEventBase>(l);
			System.Int32 ret=self.GetPersistentEventCount();
			pushValue(l,ret);
			return 1;
		}
		catch(Exception e) {
			LuaDLL.luaL_error(l, e.ToString());
			return 0;
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int GetPersistentTarget(IntPtr l) {
		try{
			UnityEngine.Events.UnityEventBase self=checkSelf<UnityEngine.Events.UnityEventBase>(l);
			System.Int32 a1;
			checkType(l,2,out a1);
			UnityEngine.Object ret=self.GetPersistentTarget(a1);
			pushValue(l,ret);
			return 1;
		}
		catch(Exception e) {
			LuaDLL.luaL_error(l, e.ToString());
			return 0;
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int GetPersistentMethodName(IntPtr l) {
		try{
			UnityEngine.Events.UnityEventBase self=checkSelf<UnityEngine.Events.UnityEventBase>(l);
			System.Int32 a1;
			checkType(l,2,out a1);
			System.String ret=self.GetPersistentMethodName(a1);
			pushValue(l,ret);
			return 1;
		}
		catch(Exception e) {
			LuaDLL.luaL_error(l, e.ToString());
			return 0;
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int SetPersistentListenerState(IntPtr l) {
		try{
			UnityEngine.Events.UnityEventBase self=checkSelf<UnityEngine.Events.UnityEventBase>(l);
			System.Int32 a1;
			checkType(l,2,out a1);
			UnityEngine.Events.UnityEventCallState a2;
			checkType(l,3,out a2);
			self.SetPersistentListenerState(a1,a2);
			return 0;
		}
		catch(Exception e) {
			LuaDLL.luaL_error(l, e.ToString());
			return 0;
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int RemoveAllListeners(IntPtr l) {
		try{
			UnityEngine.Events.UnityEventBase self=checkSelf<UnityEngine.Events.UnityEventBase>(l);
			self.RemoveAllListeners();
			return 0;
		}
		catch(Exception e) {
			LuaDLL.luaL_error(l, e.ToString());
			return 0;
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int GetValidMethodInfo(IntPtr l) {
		try{
			System.Object a1;
			checkType(l,1,out a1);
			System.String a2;
			checkType(l,2,out a2);
			System.Type[] a3;
			checkType(l,3,out a3);
			System.Reflection.MethodInfo ret=UnityEngine.Events.UnityEventBase.GetValidMethodInfo(a1,a2,a3);
			pushValue(l,ret);
			return 1;
		}
		catch(Exception e) {
			LuaDLL.luaL_error(l, e.ToString());
			return 0;
		}
	}
	static public void reg(IntPtr l) {
		getTypeTable(l,"UnityEngine.Events.UnityEventBase");
		addMember(l,GetPersistentEventCount, "GetPersistentEventCount");
		addMember(l,GetPersistentTarget, "GetPersistentTarget");
		addMember(l,GetPersistentMethodName, "GetPersistentMethodName");
		addMember(l,SetPersistentListenerState, "SetPersistentListenerState");
		addMember(l,RemoveAllListeners, "RemoveAllListeners");
		addMember(l,GetValidMethodInfo, "GetValidMethodInfo");
		newType(l, constructor);
		createTypeMetatable(l, typeof(UnityEngine.Events.UnityEventBase));
		LuaDLL.lua_pop(l, 1);
	}
}
