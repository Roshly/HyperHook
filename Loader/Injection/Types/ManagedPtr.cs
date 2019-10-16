using System;
using System.Runtime.InteropServices;

namespace Injection.Types {
    public class ManagedPtr<T> where T : struct {
        public IntPtr Address { get; }

        public T Value => this[0];

        int? structSize;

        public int StructSize {
            get {
                if (structSize == null) {
                    structSize = Marshal.SizeOf(typeof(T));
                }

                return structSize.Value;
            }
        }

        public ManagedPtr(IntPtr address) {
            Address = address;
        }

        public ManagedPtr(object value, bool freeHandle = true) {
            if (value == null) {
                throw new InvalidOperationException("Cannot create a pointer of type null");
            }

            try {
                handle = GCHandle.Alloc(value, GCHandleType.Pinned);
            }
            catch (Exception) {
                throw new InvalidOperationException($"Cannot create a pointer of type {value.GetType().Name}");
            }

            this.freeHandle = freeHandle;
            Address = handle.AddrOfPinnedObject();
        }

        static T GetStructure(IntPtr address) => (T)Marshal.PtrToStructure(address, typeof(T));

        public T this[uint index] {
            get { return GetStructure(Address + (int)index * StructSize); }
        }

        public static ManagedPtr<T> operator +(ManagedPtr<T> c1, int c2) {
            return new ManagedPtr<T>(c1.Address + c2 * c1.StructSize);
        }

        public static ManagedPtr<T> operator ++(ManagedPtr<T> a) {
            return a + 1;
        }

        public static ManagedPtr<T> operator -(ManagedPtr<T> c1, int c2) {
            return new ManagedPtr<T>(c1.Address - c2 * c1.StructSize);
        }

        public static ManagedPtr<T> operator --(ManagedPtr<T> a) {
            return a - 1;
        }

        public static explicit operator ManagedPtr<T>(IntPtr ptr) {
            if (ptr == IntPtr.Zero) {
                return null;
            }

            return new ManagedPtr<T>(ptr);
        }

        public static explicit operator IntPtr(ManagedPtr<T> ptr) {
            return ptr.Address;
        }

        GCHandle handle;

        bool freeHandle;

        ~ManagedPtr() {
            if (handle.IsAllocated && freeHandle) {
                handle.Free();
            }
        }
    }
}