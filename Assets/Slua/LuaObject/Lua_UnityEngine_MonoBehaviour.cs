﻿using UnityEngine;
using System;
using LuaInterface;
using SLua;
using System.Collections.Generic;
public class Lua_UnityEngine_MonoBehaviour : LuaObject {
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int constructor(IntPtr l) {
		LuaDLL.lua_remove(l,1);
		UnityEngine.MonoBehaviour o;
		if(matchType(l,1)){
			o=new UnityEngine.MonoBehaviour();
			pushObject(l,o);
			return 1;
		}
		return 0;
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int Invoke(IntPtr l) {
		try{
			UnityEngine.MonoBehaviour self=checkSelf<UnityEngine.MonoBehaviour>(l);
			System.String a1;
			checkType(l,2,out a1);
			System.Single a2;
			checkType(l,3,out a2);
			self.Invoke(a1,a2);
			return 0;
		}
		catch(Exception e) {
			LuaDLL.luaL_error(l, e.ToString());
			return 0;
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int InvokeRepeating(IntPtr l) {
		try{
			UnityEngine.MonoBehaviour self=checkSelf<UnityEngine.MonoBehaviour>(l);
			System.String a1;
			checkType(l,2,out a1);
			System.Single a2;
			checkType(l,3,out a2);
			System.Single a3;
			checkType(l,4,out a3);
			self.InvokeRepeating(a1,a2,a3);
			return 0;
		}
		catch(Exception e) {
			LuaDLL.luaL_error(l, e.ToString());
			return 0;
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int CancelInvoke(IntPtr l) {
		try{
			if(matchType(l,2)){
				UnityEngine.MonoBehaviour self=checkSelf<UnityEngine.MonoBehaviour>(l);
				self.CancelInvoke();
				return 0;
			}
			else if(matchType(l,2,typeof(System.String))){
				UnityEngine.MonoBehaviour self=checkSelf<UnityEngine.MonoBehaviour>(l);
				System.String a1;
				checkType(l,2,out a1);
				self.CancelInvoke(a1);
				return 0;
			}
			LuaDLL.luaL_error(l,"No matched override function to call");
			return 0;
		}
		catch(Exception e) {
			LuaDLL.luaL_error(l, e.ToString());
			return 0;
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int IsInvoking(IntPtr l) {
		try{
			if(matchType(l,2,typeof(System.String))){
				UnityEngine.MonoBehaviour self=checkSelf<UnityEngine.MonoBehaviour>(l);
				System.String a1;
				checkType(l,2,out a1);
				System.Boolean ret=self.IsInvoking(a1);
				pushValue(l,ret);
				return 1;
			}
			else if(matchType(l,2)){
				UnityEngine.MonoBehaviour self=checkSelf<UnityEngine.MonoBehaviour>(l);
				System.Boolean ret=self.IsInvoking();
				pushValue(l,ret);
				return 1;
			}
			LuaDLL.luaL_error(l,"No matched override function to call");
			return 0;
		}
		catch(Exception e) {
			LuaDLL.luaL_error(l, e.ToString());
			return 0;
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int StartCoroutine(IntPtr l) {
		try{
			if(matchType(l,2,typeof(System.Collections.IEnumerator))){
				UnityEngine.MonoBehaviour self=checkSelf<UnityEngine.MonoBehaviour>(l);
				System.Collections.IEnumerator a1;
				checkType(l,2,out a1);
				UnityEngine.Coroutine ret=self.StartCoroutine(a1);
				pushValue(l,ret);
				return 1;
			}
			else if(matchType(l,2,typeof(System.String),typeof(System.Object))){
				UnityEngine.MonoBehaviour self=checkSelf<UnityEngine.MonoBehaviour>(l);
				System.String a1;
				checkType(l,2,out a1);
				System.Object a2;
				checkType(l,3,out a2);
				UnityEngine.Coroutine ret=self.StartCoroutine(a1,a2);
				pushValue(l,ret);
				return 1;
			}
			else if(matchType(l,2,typeof(System.String))){
				UnityEngine.MonoBehaviour self=checkSelf<UnityEngine.MonoBehaviour>(l);
				System.String a1;
				checkType(l,2,out a1);
				UnityEngine.Coroutine ret=self.StartCoroutine(a1);
				pushValue(l,ret);
				return 1;
			}
			LuaDLL.luaL_error(l,"No matched override function to call");
			return 0;
		}
		catch(Exception e) {
			LuaDLL.luaL_error(l, e.ToString());
			return 0;
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int StartCoroutine_Auto(IntPtr l) {
		try{
			UnityEngine.MonoBehaviour self=checkSelf<UnityEngine.MonoBehaviour>(l);
			System.Collections.IEnumerator a1;
			checkType(l,2,out a1);
			UnityEngine.Coroutine ret=self.StartCoroutine_Auto(a1);
			pushValue(l,ret);
			return 1;
		}
		catch(Exception e) {
			LuaDLL.luaL_error(l, e.ToString());
			return 0;
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int StopCoroutine(IntPtr l) {
		try{
			if(matchType(l,2,typeof(System.String))){
				UnityEngine.MonoBehaviour self=checkSelf<UnityEngine.MonoBehaviour>(l);
				System.String a1;
				checkType(l,2,out a1);
				self.StopCoroutine(a1);
				return 0;
			}
			else if(matchType(l,2,typeof(System.Collections.IEnumerator))){
				UnityEngine.MonoBehaviour self=checkSelf<UnityEngine.MonoBehaviour>(l);
				System.Collections.IEnumerator a1;
				checkType(l,2,out a1);
				self.StopCoroutine(a1);
				return 0;
			}
			else if(matchType(l,2,typeof(UnityEngine.Coroutine))){
				UnityEngine.MonoBehaviour self=checkSelf<UnityEngine.MonoBehaviour>(l);
				UnityEngine.Coroutine a1;
				checkType(l,2,out a1);
				self.StopCoroutine(a1);
				return 0;
			}
			LuaDLL.luaL_error(l,"No matched override function to call");
			return 0;
		}
		catch(Exception e) {
			LuaDLL.luaL_error(l, e.ToString());
			return 0;
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int StopAllCoroutines(IntPtr l) {
		try{
			UnityEngine.MonoBehaviour self=checkSelf<UnityEngine.MonoBehaviour>(l);
			self.StopAllCoroutines();
			return 0;
		}
		catch(Exception e) {
			LuaDLL.luaL_error(l, e.ToString());
			return 0;
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int print(IntPtr l) {
		try{
			System.Object a1;
			checkType(l,1,out a1);
			UnityEngine.MonoBehaviour.print(a1);
			return 0;
		}
		catch(Exception e) {
			LuaDLL.luaL_error(l, e.ToString());
			return 0;
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int get_useGUILayout(IntPtr l) {
		UnityEngine.MonoBehaviour o = checkSelf<UnityEngine.MonoBehaviour>(l);
		pushValue(l,o.useGUILayout);
		return 1;
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int set_useGUILayout(IntPtr l) {
		UnityEngine.MonoBehaviour o = checkSelf<UnityEngine.MonoBehaviour>(l);
		System.Boolean v;
		checkType(l,2,out v);
		o.useGUILayout=v;
		return 0;
	}
	static public void reg(IntPtr l) {
		getTypeTable(l,"UnityEngine.MonoBehaviour");
		addMember(l,Invoke, "Invoke");
		addMember(l,InvokeRepeating, "InvokeRepeating");
		addMember(l,CancelInvoke, "CancelInvoke");
		addMember(l,IsInvoking, "IsInvoking");
		addMember(l,StartCoroutine, "StartCoroutine");
		addMember(l,StartCoroutine_Auto, "StartCoroutine_Auto");
		addMember(l,StopCoroutine, "StopCoroutine");
		addMember(l,StopAllCoroutines, "StopAllCoroutines");
		addMember(l,print, "print");
		addMember(l,get_useGUILayout, "get_useGUILayout");
		addMember(l,set_useGUILayout, "set_useGUILayout");
		newType(l, constructor);
		createTypeMetatable(l, typeof(UnityEngine.MonoBehaviour),typeof(UnityEngine.Behaviour));
		LuaDLL.lua_pop(l, 1);
	}
}
