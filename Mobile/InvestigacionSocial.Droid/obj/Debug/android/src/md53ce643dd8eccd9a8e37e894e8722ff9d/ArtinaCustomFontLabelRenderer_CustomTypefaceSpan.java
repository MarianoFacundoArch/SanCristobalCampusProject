package md53ce643dd8eccd9a8e37e894e8722ff9d;


public class ArtinaCustomFontLabelRenderer_CustomTypefaceSpan
	extends android.text.style.TypefaceSpan
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_updateDrawState:(Landroid/text/TextPaint;)V:GetUpdateDrawState_Landroid_text_TextPaint_Handler\n" +
			"n_updateMeasureState:(Landroid/text/TextPaint;)V:GetUpdateMeasureState_Landroid_text_TextPaint_Handler\n" +
			"";
		mono.android.Runtime.register ("UXDivers.Artina.Shared.ArtinaCustomFontLabelRenderer+CustomTypefaceSpan, UXDivers.Artina.Shared.Droid, Version=2.6.9.0, Culture=neutral, PublicKeyToken=null", ArtinaCustomFontLabelRenderer_CustomTypefaceSpan.class, __md_methods);
	}


	public ArtinaCustomFontLabelRenderer_CustomTypefaceSpan (android.os.Parcel p0)
	{
		super (p0);
		if (getClass () == ArtinaCustomFontLabelRenderer_CustomTypefaceSpan.class)
			mono.android.TypeManager.Activate ("UXDivers.Artina.Shared.ArtinaCustomFontLabelRenderer+CustomTypefaceSpan, UXDivers.Artina.Shared.Droid, Version=2.6.9.0, Culture=neutral, PublicKeyToken=null", "Android.OS.Parcel, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065", this, new java.lang.Object[] { p0 });
	}


	public ArtinaCustomFontLabelRenderer_CustomTypefaceSpan (java.lang.String p0)
	{
		super (p0);
		if (getClass () == ArtinaCustomFontLabelRenderer_CustomTypefaceSpan.class)
			mono.android.TypeManager.Activate ("UXDivers.Artina.Shared.ArtinaCustomFontLabelRenderer+CustomTypefaceSpan, UXDivers.Artina.Shared.Droid, Version=2.6.9.0, Culture=neutral, PublicKeyToken=null", "System.String, mscorlib, Version=2.0.5.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e", this, new java.lang.Object[] { p0 });
	}


	public void updateDrawState (android.text.TextPaint p0)
	{
		n_updateDrawState (p0);
	}

	private native void n_updateDrawState (android.text.TextPaint p0);


	public void updateMeasureState (android.text.TextPaint p0)
	{
		n_updateMeasureState (p0);
	}

	private native void n_updateMeasureState (android.text.TextPaint p0);

	private java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
