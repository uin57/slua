﻿using UnityEngine;
using System;
using LuaInterface;
using SLua;
using System.Collections.Generic;
public class Lua_UnityEngine_LightShadows : LuaObject {
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int IntToEnum(IntPtr l) {
		int v = LuaDLL.lua_tointeger(l, 1);
		UnityEngine.LightShadows o = (UnityEngine.LightShadows)v;
		pushValue(l,o);
		return 1;
	}
	static public void reg(IntPtr l) {
		getTypeTable(l,"UnityEngine.LightShadows");
		LuaDLL.lua_pushinteger(l, 0);
		LuaDLL.lua_setfield(l, -2, "None");
		LuaDLL.lua_pushinteger(l, 1);
		LuaDLL.lua_setfield(l, -2, "Hard");
		LuaDLL.lua_pushinteger(l, 2);
		LuaDLL.lua_setfield(l, -2, "Soft");
		addMember(l,IntToEnum, "IntToEnum");
		LuaDLL.lua_pop(l, 1);
	}
}
