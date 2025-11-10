
using System;
using System.Text;

namespace NImpeller;
public partial class ImpellerParagraphBuilder
{
    /// <summary>
    /// Adds UTF-8 text to the paragraph with the current style.
    /// </summary>
    /// <param name="text">The text to add. Can contain Unicode characters, emoji, etc.</param>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="text"/> is null.</exception>
    /// <exception cref="ObjectDisposedException">Thrown when the builder has been disposed.</exception>
    /// <remarks>
    /// <para>
    /// The text is automatically encoded as UTF-8 before being passed to the native API.
    /// All Unicode characters including emoji and complex scripts are supported.
    /// </para>
    /// <para>
    /// The text is styled with the current top style on the style stack.
    /// Push styles with <see cref="ImpellerParagraphStyle.PushStyle"/> before adding text to apply styling.
    /// </para>
    /// </remarks>
    public void AddText(string text)
    {
        if (text == null)
        {
            throw new ArgumentNullException(nameof(text));
        }

        if (Handle.IsClosed)
        {
            throw new ObjectDisposedException(nameof(ImpellerParagraphBuilder));
        }

        unsafe
        {
            var utf8Bytes = Encoding.UTF8.GetBytes(text);
            fixed (byte* ptr = utf8Bytes)
            {
                UnsafeNativeMethods.ImpellerParagraphBuilderAddText(
                    Handle,
                    ptr,
                    (uint)utf8Bytes.Length);
            }
        }
    }
}