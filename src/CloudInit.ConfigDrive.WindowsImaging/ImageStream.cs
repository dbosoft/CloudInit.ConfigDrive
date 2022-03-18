using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;

namespace Dbosoft.CloudInit.ConfigDrive
{
    /// <summary>
    /// This is a representation of an IO.Stream and IStream object. 
    /// </summary>
    internal class ImageStream : Stream, IStream, IDisposable
    {
        /// <summary>
        /// Gets a value indicating whether the current stream supports reading.
        /// </summary>
        public override bool CanRead
        {
            get
            {
                if (_imageStream != null)
                {
                    return true;
                }
                else
                {
                    return _stream.CanRead;
                }
            }
        }

        /// <summary>
        /// Gets a value indicating whether the current stream supports seeking.
        /// </summary>
        public override bool CanSeek
        {
            get
            {
                if (_imageStream != null)
                {
                    return true;
                }
                else
                {
                    return _stream.CanSeek;
                }
            }
        }

        /// <summary>
        /// Gets a value indicating whether the current stream supports writing.
        /// </summary>
        public override bool CanWrite
        {
            get
            {
                if (_imageStream != null)
                {
                    return true;
                }
                else
                {
                    return _stream.CanWrite;
                }
            }
        }

        public override bool CanTimeout
        {
            get
            {
                if (_imageStream != null)
                {
                    return false;
                }
                else
                {
                    return _stream.CanTimeout;
                }
            }
        }

        /// <summary>
        /// Gets the length in bytes of the stream.
        /// </summary>
        public override long Length
        {
            get
            {
                if (_imageStream != null)
                {
                    // Call IStream.Stat to retrieve info about the stream,
                    // which includes the length. STATFLAG_NONAME means that we don't
                    // care about the name (STATSTG.pwcsName), so there is no need for
                    // the method to allocate memory for the string.
                    _imageStream.Stat(out var statstg, 1);
                    return statstg.cbSize;
                }
                else
                {
                    return _stream.Length;
                }
            }
        }

        /// <summary>
        /// Gets or sets the position within the current stream.
        /// </summary>
        public override long Position
        {
            get
            {
                if (_imageStream != null)
                {
                    return Seek(0, SeekOrigin.Current);
                }
                else
                {
                    return _stream.Position;
                }
            }
            set
            {
                if (_imageStream != null)
                {
                    Seek(value, SeekOrigin.Begin);
                }
                else
                {
                    _stream.Position = value;
                }
            }
        }

        /// <summary>
        /// Clears all buffers for this stream and causes any buffered data to be written 
        /// to the underlying device.
        /// </summary>
        public override void Flush()
        {
            if (_imageStream != null)
            {
                _imageStream.Commit(0 /*STGC_DEFAULT*/);
            }
            else
            {
                _stream.Flush();
            }
        }

        /// <summary>
        /// Reads a sequence of bytes from the current stream and advances the position 
        /// within the stream by the number of bytes read.
        /// </summary>
        /// <param name="buffer">An array of bytes. When this method returns, the buffer contains the specified byte array with the values between offset and (offset + count - 1) replaced by the bytes read from the current source.</param>
        /// <param name="offset">The zero-based byte offset in buffer at which to begin storing the data read from the current stream.</param>
        /// <param name="count">The maximum number of bytes to be read from the current stream.</param>
        /// <returns>The total number of bytes read into the buffer. This can be less than the number of bytes requested if that many bytes are not currently available, or zero (0) if the end of the stream has been reached.</returns>
        public override int Read(byte[] buffer, int offset, int count)
        {
            if (_imageStream != null)
            {
                if (offset != 0) throw new NotSupportedException("Only a zero offset is supported.");

                var bytesRead = 0;
                var br = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(int)));
                Marshal.WriteInt32(br, 0);

                // Include try catch for c++ eh exceptions. are they the same as comexceptions?
                _imageStream.Read(buffer, count, br);
                bytesRead = Marshal.ReadInt32(br);

                Marshal.FreeHGlobal(br);

                return bytesRead;
            }
            else
            {
                return _stream.Read(buffer, offset, count);
            }
        }

        /// <summary>
        /// Sets the position within the current stream.
        /// </summary>
        /// <param name="offset">A byte offset relative to the origin parameter.</param>
        /// <param name="origin">A value of type SeekOrigin indicating the reference point used to obtain the new position.</param>
        /// <returns>The new position within the current stream.</returns>
        public override long Seek(long offset, System.IO.SeekOrigin origin)
        {
            if (_imageStream != null)
            {
                long position = 0;
                var pos = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(long)));
                Marshal.WriteInt64(pos, 0);

                // The enum values of SeekOrigin match the enum values of
                // STREAM_SEEK, so we can just cast the origin to an integer.
                _imageStream.Seek(offset, (int)origin, pos);
                position = Marshal.ReadInt64(pos);

                Marshal.FreeHGlobal(pos);

                return position;
            }
            else
            {
                return _stream.Seek(offset, origin);
            }
        }

        /// <summary>
        /// Sets the length of the current stream.
        /// </summary>
        /// <param name="value">The desired length of the current stream in bytes.</param>
        public override void SetLength(long value)
        {
            if (_imageStream != null)
            {
                _imageStream.SetSize(value);
            }
            else
            {
                _stream.SetLength(value);
            }
        }

        // Writes a sequence of bytes to the current stream and advances the
        // current position within this stream by the number of bytes written
        /// <summary>
        /// Writes a sequence of bytes to the current stream and advances the current position 
        /// within this stream by the number of bytes written.
        /// </summary>
        /// <param name="buffer">An array of bytes. This method copies count bytes from buffer to the current stream.</param>
        /// <param name="offset">The zero-based byte offset in buffer at which to begin copying bytes to the current stream.</param>
        /// <param name="count">The number of bytes to be written to the current stream.</param>
        public override void Write(byte[] buffer, int offset, int count)
        {
            if (_imageStream != null)
            {
                if (offset != 0) throw new NotSupportedException("Only a zero offset is supported.");

                // Pass "null" for the last parameter since we don't use the value
                _imageStream.Write(buffer, count, IntPtr.Zero);
            }
            else
            {
                _stream.Write(buffer, offset, count);
            }
        }

        /// <summary>
        /// Creates a new stream object with its own seek pointer that references 
        /// the same bytes as the original stream.
        /// </summary>
        /// <remarks>
        /// This method is not used and always throws the exception.
        /// </remarks>
        /// <param name="ppstm">When successful, pointer to the location of an IStream pointer to the new stream object.</param>
        ///<exception cref="NotSupportedException">The IO.Streamtream cannot be cloned.</exception>
        public void Clone(out IStream ppstm)
        {
            if (_stream != null) throw new NotSupportedException("The Stream cannot be cloned.");

            _imageStream.Clone(out ppstm);
        }

        /// <summary>
        /// Ensures that any changes made to an stream object that is open in transacted 
        /// mode are reflected in the parent storage.
        /// </summary>
        /// <remarks>
        /// The <paramref name="grfCommitFlags"/> parameter is not used and this method only does Stream.Flush()
        /// </remarks>
        /// <param name="grfCommitFlags">Controls how the changes for the stream object are committed. 
        /// See the STGC enumeration for a definition of these values.</param>
        ///<exception cref="IOException">An I/O error occurs.</exception>
        public void Commit(int grfCommitFlags)
        {
            // Clears all buffers for this stream and causes any buffered data to be written 
            // to the underlying device.
            if (_stream != null)
            {
                _stream.Flush();
            }
            else
            {
                _imageStream.Commit(grfCommitFlags);
            }
        }

        ///  <summary>
        ///  Copies a specified number of bytes from the current seek pointer in the stream 
        ///  to the current seek pointer in another stream.
        ///  </summary>
        ///  <param name="pstm">
        ///  The destination stream. The pstm stream  can be a new stream or a clone of the source stream.
        ///  </param>
        ///  <param name="cb">
        ///  The number of bytes to copy from the source stream.
        ///  </param>
        ///  <param name="pcbRead">
        ///  The actual number of bytes read from the source. 
        ///  It can be set to IntPtr.Zero. 
        ///  In this case, this method does not provide the actual number of bytes read.
        ///  </param>
        ///  <param name="pcbWritten">
        ///  The actual number of bytes written to the destination. 
        ///  It can be set this to IntPtr.Zero. 
        ///  In this case, this method does not provide the actual number of bytes written.
        ///  </param>
        ///  <returns>
        ///  The actual number of bytes read (<paramref name="pcbRead"/>) and written (<paramref name="pcbWritten"/>) from the source.
        ///  </returns>
        /// <exception cref="ArgumentException">The sum of offset and count is larger than the buffer length.</exception>
        /// <exception cref="ArgumentNullException">buffer is a null reference.</exception>
        /// <exception cref="ArgumentOutOfRangeException">offset or count is negative.</exception>
        /// <exception cref="IOException">An I/O error occurs.</exception>
        /// <exception cref="NotSupportedException">The stream does not support reading.</exception>
        /// <exception cref="ObjectDisposedException">Methods were called after the stream was closed.</exception>
        public void CopyTo(IStream pstm, long cb, IntPtr pcbRead, IntPtr pcbWritten)
        {
            if (_stream != null)
            {
                var sourceBytes = new byte[cb];
                long totalBytesRead = 0;
                long totalBytesWritten = 0;

                var bw = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(int)));
                Marshal.WriteInt32(bw, 0);

                while (totalBytesWritten < cb)
                {
                    var currentBytesRead = _stream.Read(sourceBytes, 0, (int)(cb - totalBytesWritten));

                    // Has the end of the stream been reached?
                    if (currentBytesRead == 0) break;

                    totalBytesRead += currentBytesRead;

                    pstm.Write(sourceBytes, currentBytesRead, bw);
                    var currentBytesWritten = Marshal.ReadInt32(bw);
                    if (currentBytesWritten != currentBytesRead)
                    {
                        Debug.WriteLine("ERROR!: The IStream Write is not writing all the bytes needed!");
                    }
                    totalBytesWritten += currentBytesWritten;
                }

                Marshal.FreeHGlobal(bw);

                if (pcbRead != IntPtr.Zero) Marshal.WriteInt64(pcbRead, totalBytesRead);
                if (pcbWritten != IntPtr.Zero) Marshal.WriteInt64(pcbWritten, totalBytesWritten);
            }
            else
            {
                _imageStream.CopyTo(pstm, cb, pcbRead, pcbWritten);
            }
        }

        /// <summary>
        /// Restricts access to a specified range of bytes in the stream.
        /// </summary>
        /// <remarks>
        /// This method is not used and always throws the exception.
        /// </remarks>
        /// <param name="libOffset">Integer that specifies the byte offset for the beginning of the range.</param>
        /// <param name="cb">Integer that specifies the length of the range, in bytes, to be restricted.</param>
        /// <param name="dwLockType">Specifies the restrictions being requested on accessing the range.</param>
        ///<exception cref="NotSupportedException">The IO.Stream does not support locking.</exception>
        public void LockRegion(long libOffset, long cb, int dwLockType)
        {
            if (_stream != null) throw new NotSupportedException("Stream does not support locking.");

            _imageStream.LockRegion(libOffset, cb, dwLockType);
        }

        ///  <summary>
        ///  Reads a specified number of bytes from the stream object 
        ///  into memory starting at the current seek pointer.
        ///  </summary>
        ///  <param name="pv">The buffer which the stream data is read into.</param>
        ///  <param name="cb">The number of bytes of data to read from the stream object.</param>
        ///  <param name="pcbRead">
        ///  A pointer to a ULONG variable that receives the actual number of bytes read from the stream object.
        ///  It can be set to IntPtr.Zero. 
        ///  In this case, this method does not return the number of bytes read.
        ///  </param>
        ///  <returns>
        ///  The actual number of bytes read (<paramref name="pcbRead"/>) from the source.
        ///  </returns>
        /// <exception cref="ArgumentException">The sum of offset and count is larger than the buffer length.</exception>
        /// <exception cref="ArgumentNullException">buffer is a null reference.</exception>
        /// <exception cref="ArgumentOutOfRangeException">offset or count is negative.</exception>
        /// <exception cref="IOException">An I/O error occurs.</exception>
        /// <exception cref="NotSupportedException">The stream does not support reading.</exception>
        /// <exception cref="ObjectDisposedException">Methods were called after the stream was closed.</exception>
        public void Read(byte[] pv, int cb, IntPtr pcbRead)
        {
            if (_stream != null)
            {
                if (pcbRead == IntPtr.Zero)
                {
                    // User isn't interested in how many bytes were read
                    _stream.Read(pv, 0, cb);
                }
                else
                {
                    Marshal.WriteInt32(pcbRead, _stream.Read(pv, 0, cb));
                }
            }
            else
            {
                _imageStream.Read(pv, cb, pcbRead);
            }
        }

        /// <summary>
        /// Discards all changes that have been made to a transacted 
        /// stream since the last stream.Commit call
        /// </summary>
        /// <remarks>
        /// This method is not used and always throws the exception.
        /// </remarks>
        ///<exception cref="NotSupportedException">The IO.Stream does not support reverting.</exception>
        public void Revert()
        {
            if (_stream != null) throw new NotSupportedException("Stream does not support reverting.");

            _imageStream.Revert();
        }

        ///  <summary>
        ///  Changes the seek pointer to a new location relative to the beginning
        /// of the stream, the end of the stream, or the current seek pointer
        ///  </summary>
        ///  <param name="dlibMove">
        ///  The displacement to be added to the location indicated by the dwOrigin parameter. 
        ///  If dwOrigin is STREAM_SEEK_SET, this is interpreted as an unsigned value rather than a signed value.
        ///  </param>
        ///  <param name="dwOrigin">
        ///  The origin for the displacement specified in dlibMove. 
        ///  The origin can be the beginning of the file (STREAM_SEEK_SET), the current seek pointer (STREAM_SEEK_CUR), or the end of the file (STREAM_SEEK_END).
        ///  </param>
        ///  <param name="plibNewPosition">
        ///  The location where this method writes the value of the new seek pointer from the beginning of the stream.
        ///  It can be set to IntPtr.Zero. In this case, this method does not provide the new seek pointer.
        ///  </param>
        ///  <returns>
        ///  Returns in <paramref name="plibNewPosition"/> the location where this method writes the value of the new seek pointer from the beginning of the stream.
        ///  </returns>
        /// <exception cref="IOException">An I/O error occurs.</exception>
        /// <exception cref="NotSupportedException">The stream does not support reading.</exception>
        /// <exception cref="ObjectDisposedException">Methods were called after the stream was closed.</exception>
        public void Seek(long dlibMove, int dwOrigin, IntPtr plibNewPosition)
        {
            if (_stream != null)
            {
                // The enum values of SeekOrigin match the enum values of
                // STREAM_SEEK, so we can just cast the dwOrigin to a SeekOrigin

                if (plibNewPosition == IntPtr.Zero)
                {
                    // User isn't interested in new position
                    _stream.Seek(dlibMove, (SeekOrigin)dwOrigin);
                }
                else
                {
                    var origin = (SeekOrigin)dwOrigin;
                    if (origin != SeekOrigin.Begin &&
                        origin != SeekOrigin.Current &&
                        origin != SeekOrigin.End)
                    {
                        origin = SeekOrigin.Begin;
                    }
                    Marshal.WriteInt64(plibNewPosition, _stream.Seek(dlibMove, origin));
                }
            }
            else
            {
                _imageStream.Seek(dlibMove, dwOrigin, plibNewPosition);
            }
        }

        /// <summary>
        /// Changes the size of the stream object.
        /// </summary>
        /// <param name="libNewSize">Specifies the new size of the stream as a number of bytes.</param>
        ///<exception cref="IOException">An I/O error occurs.</exception>
        ///<exception cref="NotSupportedException">The stream does not support reading.</exception>
        ///<exception cref="ObjectDisposedException">Methods were called after the stream was closed.</exception>
        public void SetSize(long libNewSize)
        {
            if (_stream != null)
            {
                // Sets the length of the current stream.
                _stream.SetLength(libNewSize);
            }
            else
            {
                _imageStream.SetSize(libNewSize);
            }
        }

        /// <summary>
        /// Retrieves the STATSTG structure for this stream.
        /// </summary>
        /// <remarks>
        /// The <paramref name="grfStatFlag"/> parameter is not used
        /// </remarks>
        /// <param name="pstatstg">
        /// The STATSTG structure where this method places information about this stream object.
        /// </param>
        /// <param name="grfStatFlag">
        /// Specifies that this method does not return some of the members in the STATSTG structure, 
        /// thus saving a memory allocation operation. This parameter is not used internally.
        /// </param>
        ///<exception cref="NotSupportedException">The stream does not support reading.</exception>
        ///<exception cref="ObjectDisposedException">Methods were called after the stream was closed.</exception>
        public void Stat(out System.Runtime.InteropServices.ComTypes.STATSTG pstatstg, int grfStatFlag)
        {
            if (_stream != null)
            {
                pstatstg = new System.Runtime.InteropServices.ComTypes.STATSTG
                {
                    type = 2, cbSize = _stream.Length, grfMode = 2, grfLocksSupported = 2
                };
                // STGTY_STREAM
                // Gets the length in bytes of the stream.
                // STGM_READWRITE;
                // LOCK_EXCLUSIVE
            }
            else
            {
                _imageStream.Stat(out pstatstg, grfStatFlag);
            }
        }

        /// <summary>
        /// Removes the access restriction on a range of bytes previously 
        /// restricted with the LockRegion method.
        /// </summary>
        /// <remarks>
        /// This method is not used and always throws the exception.
        /// </remarks>
        /// <param name="libOffset">Specifies the byte offset for the beginning of the range.</param>
        /// <param name="cb">Specifies, in bytes, the length of the range to be restricted.</param>
        /// <param name="dwLockType">Specifies the access restrictions previously placed on the range.</param>
        ///<exception cref="NotSupportedException">The IO.Stream does not support unlocking.</exception>
        public void UnlockRegion(long libOffset, long cb, int dwLockType)
        {
            if (_stream != null) throw new NotSupportedException("Stream does not support unlocking.");

            _imageStream.UnlockRegion(libOffset, cb, dwLockType);
        }

        ///  <summary>
        ///  Writes a specified number of bytes into the stream object 
        /// starting at the current seek pointer.
        ///  </summary>
        ///  <param name="pv">The buffer that contains the data that is to be written to the stream. 
        ///  A valid buffer must be provided for this parameter even when cb is zero.</param>
        ///  <param name="cb">The number of bytes of data to attempt to write into the stream. This value can be zero.</param>
        ///  <param name="pcbWritten">
        ///  A variable where this method writes the actual number of bytes written to the stream object. 
        ///  The caller can set this to IntPtr.Zero, in which case this method does not provide the actual number of bytes written.
        ///  </param>
        ///  <returns>
        ///  The actual number of bytes written (<paramref name="pcbWritten"/>).
        ///  </returns>
        /// <exception cref="ArgumentException">The sum of offset and count is larger than the buffer length.</exception>
        /// <exception cref="ArgumentNullException">buffer is a null reference.</exception>
        /// <exception cref="ArgumentOutOfRangeException">offset or count is negative.</exception>
        /// <exception cref="IOException">An I/O error occurs.</exception>
        /// <exception cref="NotSupportedException">The IO.Stream does not support reading.</exception>
        /// <exception cref="ObjectDisposedException">Methods were called after the stream was closed.</exception>
        public void Write(byte[] pv, int cb, IntPtr pcbWritten)
        {
            if (_stream != null)
            {
                if (pcbWritten == IntPtr.Zero)
                {
                    // User isn't interested in how many bytes were written
                    _stream.Write(pv, 0, cb);
                }
                else
                {
                    var currentPosition = _stream.Position;
                    _stream.Write(pv, 0, cb);
                    Marshal.WriteInt32(pcbWritten, (int)(_stream.Position - currentPosition));
                }
            }
            else
            {
                _imageStream.Write(pv, cb, pcbWritten);
            }
        }

        // Default constructor. Should not be used to create an AStream object.
        private ImageStream()
        {
            _stream = null;
            _imageStream = null;
        }

        // Copy constructor. It is not safe to only pass the Stream and IStream.
        private ImageStream(ImageStream previousAStream)
        {
            _stream = previousAStream._stream;
            _imageStream = previousAStream._imageStream;
        }

        /// <summary>
        /// Initializes a new instance of the AStream class.
        /// </summary>
        /// <param name="stream">An IO.Stream</param>
        ///<exception cref="ArgumentNullException">Stream cannot be null</exception>
        public ImageStream(Stream stream)
        {
            this._stream = null;
            _imageStream = null;

            this._stream = stream ?? throw new ArgumentNullException("Stream cannot be null");
        }

        /// <summary>
        /// Initializes a new instance of the AStream class.
        /// </summary>
        /// <param name="stream">A ComTypes.IStream</param>
        ///<exception cref="ArgumentNullException">Stream cannot be null</exception>
        public ImageStream(IStream stream)
        {
            this._stream = null;
            _imageStream = null;

            _imageStream = stream ?? throw new ArgumentNullException("IStream cannot be null");
        }

        // Allows the Object to attempt to free resources and perform other 
        // cleanup operations before the Object is reclaimed by garbage collection. 
        // (Inherited from Object.)
        ~ImageStream()
        {
            if (_stream != null)
            {
                _stream.Close();
            }
        }

        /// <summary>
        /// Releases all resources used by the Stream object.
        /// </summary>
        void IDisposable.Dispose()
        {
            Close();
        }

        /// <summary>
        /// Closes the current stream and releases any resources 
        /// (such as the Stream) associated with the current IStream.
        /// </summary>
        /// <remarks>
        /// This method is not a member in IStream.
        /// </remarks>
        public override void Close()
        {
            if (_stream != null)
            {
                _stream.Close();
            }
            else
            {
                _imageStream.Commit(0 /*STGC_DEFAULT*/);
                //                Marshal.ReleaseComObject(TheIStream);    // Investigate this because we cannot release an IStream to the stash file
            }
            GC.SuppressFinalize(this);
        }

        /*        public static IStream ToIStream(object stream)
                {
                    IntPtr ppv;
                    IntPtr pUnk = Marshal.GetIUnknownForObject(stream);
                    Object iSteam = null;
                    Guid iid = Marshal.GenerateGuidForType(typeof(IStream));   // ComTypes.IStream GUID

                    if (Marshal.QueryInterface(pUnk, ref iid, out ppv) == 0)
                        iSteam = Marshal.GetUniqueObjectForIUnknown(ppv);

                    return (System.Runtime.InteropServices.ComTypes.IStream)iSteam;
                }*/

        public static IStream ToIStream(object stream)
        {
            switch (stream)
            {
                case Stream streamObject:
                    return new ImageStream(streamObject);
                case IStream iStream:
                    return iStream;
                default:
                    return null;
            }
        }

        public static Stream ToStream(object stream)
        {
            switch (stream)
            {
                case Stream streamObject:
                    return streamObject;
                case IStream iStream:
                    return new ImageStream(iStream);
                default:
                    return null;
            }
        }

        private readonly Stream _stream;   // The Stream being wrapped
        private readonly IStream _imageStream; // The IStream being wrapped

    }
}