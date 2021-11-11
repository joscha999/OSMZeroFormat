using System;
using System.Collections.Generic;
using System.Text;
using ZeroFormatOSM.TagConvertion;

namespace ZeroFormatOSM {
	public enum MemberRole {
		ZeroUnavailable = 0,
		Empty,

		[OSMMemberRole("forward")]
		Forward,

		[OSMMemberRole("backward")]
		Backward,

		[OSMMemberRole("TMC:Segment")]
		TMCSegment,

		[OSMMemberRole("stop")]
		Stop,

		[OSMMemberRole("platform")]
		Platform,

		[OSMMemberRole("fixme")]
		Fixme,

		[OSMMemberRole("admin_centre")]
		AdminCentre,

		[OSMMemberRole("outer")]
		Outer,

		[OSMMemberRole("start")]
		Start,

		[OSMMemberRole("via")]
		Via,

		[OSMMemberRole("from")]
		From,

		[OSMMemberRole("to")]
		To,

		[OSMMemberRole("side_stream")]
		SideStream,

		[OSMMemberRole("main_stream")]
		MainStream,

		[OSMMemberRole("tributary")]
		Tributary,

		[OSMMemberRole("inner")]
		Inner,

		[OSMMemberRole("device")]
		Device,

		[OSMMemberRole("part")]
		Part,

		[OSMMemberRole("stop_entry_only")]
		StopEntryOnly,

		[OSMMemberRole("platform_entry_only")]
		PlatformEntryOnly,

		[OSMMemberRole("label")]
		Label,

		[OSMMemberRole("section 01")]
		Section01,

		[OSMMemberRole("section 02")]
		Section02,

		[OSMMemberRole("section 03")]
		Section03,

		[OSMMemberRole("section 04")]
		Section04,

		[OSMMemberRole("section 05")]
		Section05,

		[OSMMemberRole("section 06")]
		Section06,

		[OSMMemberRole("section 07")]
		Section07,

		[OSMMemberRole("section 08")]
		Section08,

		[OSMMemberRole("section 09")]
		Section09,

		[OSMMemberRole("section 10")]
		Section10,

		[OSMMemberRole("section 11")]
		Section11,

		[OSMMemberRole("section 12")]
		Section12,

		[OSMMemberRole("section 13")]
		Section13,

		[OSMMemberRole("outline")]
		Outline,

		[OSMMemberRole("on_bridge")]
		OnBridge,
	}
}