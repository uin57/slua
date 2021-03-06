﻿using UnityEngine;
using System;
using LuaInterface;
using SLua;
using System.Collections.Generic;
public class Lua_UnityEngine_JointLimitState2D : LuaObject {
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int IntToEnum(IntPtr l) {
		int v = LuaDLL.lua_tointeger(l, 1);
		UnityEngine.JointLimitState2D o = (UnityEngine.JointLimitState2D)v;
		pushValue(l,o);
		return 1;
	}
	static public void reg(IntPtr l) {
		getTypeTable(l,"UnityEngine.JointLimitState2D");
		LuaDLL.lua_pushinteger(l, 0);
		LuaDLL.lua_setfield(l, -2, "Inactive");
		LuaDLL.lua_pushinteger(l, 1);
		LuaDLL.lua_setfield(l, -2, "LowerLimit");
		LuaDLL.lua_pushinteger(l, 2);
		LuaDLL.lua_setfield(l, -2, "UpperLimit");
		LuaDLL.lua_pushinteger(l, 3);
		LuaDLL.lua_setfield(l, -2, "EqualLimits");
		addMember(l,IntToEnum, "IntToEnum");
		LuaDLL.lua_pop(l, 1);
	}
}
