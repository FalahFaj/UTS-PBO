using System;

namespace BankPelita
{
    public abstract class Rekening  
    {
        public int No_Rek { get; set; }
        public string Nama_Pemilik { get; set; }
        public double Saldo_Rekening { get; set; }

        public abstract void Penarikan_dana(double Jumlah_Penarikan);
        public abstract void Setor_Tunai(double Jumlah_Tunai);
        public abstract void Transfer_Antar_Rek(double Nominal_Transfer, int No_Rek_Tujuan);
    }

    public class Tabungan : Rekening
    {
        public override void Penarikan_dana(double Jumlah_Penarikan)
        {
            if (Jumlah_Penarikan > Saldo_Rekening)
            {
                Console.WriteLine("Saldo tidak mencukupi untuk penarikan.");
                Console.WriteLine("Tekan sembarang tombol untuk melanjutkan");
                Console.ReadKey();
                Console.Clear();
            }
            else
            {
                Saldo_Rekening -= Jumlah_Penarikan;
                Console.WriteLine($"Penarikan Rp {Jumlah_Penarikan} berhasil. Saldo baru: Rp {Saldo_Rekening}");
                Console.WriteLine("Tekan sembarang tombol untuk melanjutkan");
                Console.ReadKey();
                Console.Clear();
            }
        }

        public override void Setor_Tunai(double Jumlah_Tunai)
        {
            Saldo_Rekening += Jumlah_Tunai;
            Console.WriteLine($"Setor tunai sebesar {Jumlah_Tunai} telah berhasil. Saldo direkening saat ini Rp {Saldo_Rekening}");
            Console.WriteLine("Tekan sembarang tombol untuk melanjutkan");
            Console.ReadKey();
            Console.Clear();
        }

        public override void Transfer_Antar_Rek(double Nominal_Transfer, int No_Rek_Tujuan)
        {
            if (Saldo_Rekening > Nominal_Transfer)
            {
                Console.WriteLine("Data transfer ");
                Console.WriteLine($"No Rekening penerima : {No_Rek_Tujuan}");
                Console.WriteLine($"Saldo yang ditransfer : {Nominal_Transfer}");
                Console.Write("Apakah sudah sesuai : ");
                string konfirmasi = Console.ReadLine();
                if (konfirmasi.ToLower() == "ya")
                {
                    Saldo_Rekening -= Nominal_Transfer;
                    Console.WriteLine($"Transfer Rp {Nominal_Transfer} ke rekening {No_Rek_Tujuan} berhasil. Saldo baru: Rp {Saldo_Rekening}");
                    Console.WriteLine("Tekan sembarang tombol untuk melanjutkan");
                    Console.ReadKey();
                    Console.Clear();
                }
                else
                {
                    Console.WriteLine("Transfer dibatalkan.");
                    Console.WriteLine("Tekan sembarang tombol untuk melanjutkan");
                    Console.ReadKey();
                    Console.Clear();
                }
            }
            else
            {
                Console.WriteLine("Saldo tidak mencukupi untuk mentransfer dana");
                Console.WriteLine("Tekan sembarang tombol untuk melanjutkan");
                Console.ReadKey();
                Console.Clear();
            }
        }
        public void Tampilkan_Data_Diri()
        {
            Console.WriteLine("Data Rekening Anda : ");
            Console.WriteLine($"No Rekening \t: {No_Rek}");
            Console.WriteLine($"Nama Pemilik \t: {Nama_Pemilik}");
            Console.WriteLine($"Saldo \t\t: {Saldo_Rekening}");
            Console.WriteLine("Tekan sembarang tombol untuk melanjutkan");
            Console.ReadKey();
            Console.Clear();
        }
    }
    public class Menu
    {
        public static void Menu_Data_Diri(Tabungan tabungan)
        {
            Console.WriteLine("Selamat Datang di Bank Pelita");
            Console.WriteLine("Silahkan masukkan data-data anda terlebih dahulu");
            Console.Write("No Rekening \t: ");
            int No_Rek = (int)Convert.ToInt64(Console.ReadLine());
            tabungan.No_Rek = No_Rek;

            Console.Write("Nama Pemilik \t: ");
            string Nama_Pemilik = Console.ReadLine();
            tabungan.Nama_Pemilik = Nama_Pemilik;
            while (true)
            {
                Console.Write("Jumlah Saldo \t: ");
                try
                {
                    double Saldo = Convert.ToDouble(Console.ReadLine());
                    tabungan.Saldo_Rekening = Saldo;
                    break;
                }
                catch (FormatException)
                {
                    Console.WriteLine("Harus berupa angka semua");
                }
            }
            Console.WriteLine("Data diri anda sudah tersimpan, silahkan pilih menu perbankan yang ada di bawah ini");
            Console.WriteLine("Tekan sembarang tombol untuk melanjutkan");
            Console.ReadKey();
            Console.Clear();
        }
        public static void Menu_Perbangkan(Tabungan tabungan)
        {
            
            bool berjalan = true;
            while (berjalan)
            {
                Console.WriteLine("Silahkan Masukkan Menu yang mau dipilih : ");
                Console.WriteLine("1. Penarikan Dana");
                Console.WriteLine("2. Setor Tunai");
                Console.WriteLine("3. Transfer Antar Rekening");
                Console.WriteLine("4. Tampilkan Data Diri");
                Console.WriteLine("5. Keluar");
                Console.Write("Pilihan Anda \t: ");
                int Pilihan = Convert.ToInt32(Console.ReadLine());
                switch (Pilihan)
                {
                    case 1:
                        Console.Write("Jumlah Penarikan \t: ");
                        double Jumlah_Penarikan = Convert.ToDouble(Console.ReadLine());
                        tabungan.Penarikan_dana(Jumlah_Penarikan);
                        break;
                    case 2:
                        Console.Write("Jumlah Setoran \t: ");
                        double Jumlah_Setoran = Convert.ToDouble(Console.ReadLine());
                        tabungan.Setor_Tunai(Jumlah_Setoran);
                        break;
                    case 3:
                        Console.Write("Jumlah Transfer \t: ");
                        double Nominal_Transfer = Convert.ToDouble(Console.ReadLine());
                        Console.Write("No Rekening Tujuan \t: ");
                        int No_Rek_Tujuan = Convert.ToInt32(Console.ReadLine());
                        tabungan.Transfer_Antar_Rek(Nominal_Transfer, No_Rek_Tujuan);
                        break;
                    case 4:
                        tabungan.Tampilkan_Data_Diri();
                        break;
                    case 5:
                        Console.WriteLine("Terima Kasih telah menggunakan layanan dari Bank Pelita");
                        berjalan = false;
                        break;
                    default:
                        Console.WriteLine("Pilihan tidak valid.");
                        break;
                }
            }
        }
    }
    class Program
    {
        static void Main()
        {
            Tabungan tabungan = new Tabungan();
            Menu.Menu_Data_Diri(tabungan);
            Menu.Menu_Perbangkan(tabungan);
        }
    }
}
