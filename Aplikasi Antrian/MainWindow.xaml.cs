using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace Aplikasi_Antrian
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        Heap<IComparable> heap = new Heap<IComparable>();
        public MainWindow()
        {
            InitializeComponent();

            
            
        }

        private async void panggil(int loket) //pilih ke loket 1/2
        {
            string nopanggil;
            //deklarasi playlist
            var playlist = new List<string>();


            if (heap.IsEmpty == false)
            {
                if (loket == 1)
                {
                    txt_loket1.Text = heap.Ambil().ToString();
                    nopanggil = txt_loket1.Text;
                }
                else
                {
                    txt_loket2.Text = heap.Ambil().ToString();
                    nopanggil = txt_loket2.Text;
                }
                

                //ambil angka nya dari P00~
                int angka = Convert.ToInt32(nopanggil.Substring(1));

                //masukin ke playlist suara NOMOR ANTRIAN
                playlist.Add(AppDomain.CurrentDomain.BaseDirectory + @"..\..\Suara\Nomor Antrian.mp3");

                //looping dari char dari loket
                for (int i = 1; i <= nopanggil.Length; i++)
                {
                    //lb_test.Content = angka;
                    //kalo angka masih satuan / nyampe sebelas
                    if (angka < 12)
                    {
                        if (angka == 10)
                        {
                            if (i == 3) //ini bakal berenti pas udah bilang P0
                            {
                                playlist.Add(AppDomain.CurrentDomain.BaseDirectory + @"..\..\Suara\10.mp3");  //panggil suara sepuluh
                                i = nopanggil.Length; //berentiin looping nya
                            }
                            else
                            {
                                playlist.Add(AppDomain.CurrentDomain.BaseDirectory + @"..\..\Suara\" + nopanggil[i - 1] + ".mp3"); //panggil suara P0
                            }

                        }
                        else if (angka == 11)
                        {
                            if (i == 3)
                            {
                                playlist.Add(AppDomain.CurrentDomain.BaseDirectory + @"..\..\Suara\11.mp3");//panggil suara sebelas
                                i = nopanggil.Length;
                            }
                            else
                            {
                                playlist.Add(AppDomain.CurrentDomain.BaseDirectory + @"..\..\Suara\" + nopanggil[i - 1] + ".mp3");
                            }
                        }
                        else
                        {
                            playlist.Add(AppDomain.CurrentDomain.BaseDirectory + @"..\..\Suara\" + nopanggil[i - 1] + ".mp3");//panggil suara angka satuan 1,2,3,4
                        }

                    }
                    //belasan
                    else if (angka < 20)
                    {
                        if (i == 3)
                        {
                            playlist.Add(AppDomain.CurrentDomain.BaseDirectory + @"..\..\Suara\" + (angka - 10) + ".mp3"); //manggil angka satuan
                            playlist.Add(AppDomain.CurrentDomain.BaseDirectory + @"..\..\Suara\belas.mp3"); //suara belas
                            i = nopanggil.Length;
                        }
                        else //buat manggil p0
                        {
                            playlist.Add(AppDomain.CurrentDomain.BaseDirectory + @"..\..\Suara\" + nopanggil[i - 1] + ".mp3");
                        }
                    }
                    //puluhan
                    else if (angka < 100)
                    {
                        if (i == 3)
                        {
                            playlist.Add(AppDomain.CurrentDomain.BaseDirectory + @"..\..\Suara\" + (angka / 10) + ".mp3");
                            playlist.Add(AppDomain.CurrentDomain.BaseDirectory + @"..\..\Suara\puluh.mp3");
                            if (angka % 10 != 0) //ini bakal berenti nyebutin di 20 30 40 dst...  
                            {
                                //panggil angka satuan nya dari sisa hasil bagi angka
                                playlist.Add(AppDomain.CurrentDomain.BaseDirectory + @"..\..\Suara\" + (angka % 10) + ".mp3");
                            }
                            i = nopanggil.Length;
                        }
                        else //buat manggil p0
                        {
                            playlist.Add(AppDomain.CurrentDomain.BaseDirectory + @"..\..\Suara\" + nopanggil[i - 1] + ".mp3");
                        }
                    }
                    //seratusan
                    else if (angka < 200)
                    {
                        if (i == 2)
                        {
                            playlist.Add(AppDomain.CurrentDomain.BaseDirectory + @"..\..\Suara\seratus.mp3");
                            if (angka % 100 != 0)
                            {
                                //panggil angka satuan nya dari sisa hasil bagi angka
                                if (angka - 100 < 12)
                                {
                                    playlist.Add(AppDomain.CurrentDomain.BaseDirectory + @"..\..\Suara\" + (angka - 100) + ".mp3");
                                }
                                else if (angka - 100 < 20)
                                {
                                    playlist.Add(AppDomain.CurrentDomain.BaseDirectory + @"..\..\Suara\" + (angka - 110) + ".mp3"); //manggil angka satuan

                                    playlist.Add(AppDomain.CurrentDomain.BaseDirectory + @"..\..\Suara\belas.mp3"); //suara belas

                                }
                                else if (angka - 100 < 100)
                                {
                                    playlist.Add(AppDomain.CurrentDomain.BaseDirectory + @"..\..\Suara\" + ((angka - 100) / 10) + ".mp3");
                                    playlist.Add(AppDomain.CurrentDomain.BaseDirectory + @"..\..\Suara\puluh.mp3");
                                    if (angka % 10 != 0) //ini bakal berenti nyebutin di 20 30 40 dst...  
                                    {
                                        //panggil angka satuan nya dari sisa hasil bagi angka
                                        playlist.Add(AppDomain.CurrentDomain.BaseDirectory + @"..\..\Suara\" + ((angka - 100) % 10) + ".mp3");
                                    }
                                }

                            }
                            i = nopanggil.Length;
                        }
                        else //buat manggil p0
                        {
                            playlist.Add(AppDomain.CurrentDomain.BaseDirectory + @"..\..\Suara\" + nopanggil[i - 1] + ".mp3");
                        }
                    }
                    else if (angka < 1000)
                    {
                        if (i == 2)
                        {

                            playlist.Add(AppDomain.CurrentDomain.BaseDirectory + @"..\..\Suara\" + ((angka / 100) % 10) + ".mp3");
                            playlist.Add(AppDomain.CurrentDomain.BaseDirectory + @"..\..\Suara\ratus.mp3");
                            if (angka % 100 != 0)
                            {
                                if (angka % 100 < 12)
                                {
                                    //txt_loket2.Text = Convert.ToString(angka % 100);
                                    playlist.Add(AppDomain.CurrentDomain.BaseDirectory + @"..\..\Suara\" + (angka % 100) + ".mp3");
                                }
                                else if (angka % 100 < 20)
                                {
                                    playlist.Add(AppDomain.CurrentDomain.BaseDirectory + @"..\..\Suara\" + ((angka % 100) - 10) + ".mp3"); //manggil angka satuan
                                    playlist.Add(AppDomain.CurrentDomain.BaseDirectory + @"..\..\Suara\belas.mp3"); //suara belas
                                }
                                else if (angka % 100 < 100)
                                {
                                    playlist.Add(AppDomain.CurrentDomain.BaseDirectory + @"..\..\Suara\" + ((angka % 100) / 10) + ".mp3");
                                    playlist.Add(AppDomain.CurrentDomain.BaseDirectory + @"..\..\Suara\puluh.mp3");
                                    if ((angka % 100) % 10 != 0)
                                    {
                                        //panggil angka satuan nya dari sisa hasil bagi angka
                                        playlist.Add(AppDomain.CurrentDomain.BaseDirectory + @"..\..\Suara\" + ((angka % 100) % 10) + ".mp3");
                                    }

                                }

                            }
                            i = nopanggil.Length;

                        }
                        else //buat manggil p0
                        {
                            playlist.Add(AppDomain.CurrentDomain.BaseDirectory + @"..\..\Suara\" + nopanggil[i - 1] + ".mp3");
                        }
                    }

                }
                //suara ke loket
                playlist.Add(AppDomain.CurrentDomain.BaseDirectory + @"..\..\Suara\ke loket.mp3");
                playlist.Add(AppDomain.CurrentDomain.BaseDirectory + @"..\..\Suara\"+loket+".mp3");

                //play semua suara nya dari playlist
                var playr = new playr(playlist);
                playr.PlaySong();

                //kasih delay biar ga terus terusan
                btn_panggil.IsEnabled = false;
                btn_panggil2.IsEnabled = false;
                await Task.Delay(7500);
                btn_panggil.IsEnabled = true;
                btn_panggil2.IsEnabled = true;

            }
            else
            {
                MessageBox.Show("Antrian Kosong");
            }
        }

        private async void Btn_panggil_Click(object sender, RoutedEventArgs e)
        {
            panggil(1);
            if (heap.IsEmpty != true)
            {
                txt_selanjutnya.Text = heap.GetMin().ToString();
            }
            else
            {
                txt_selanjutnya.Text = "....";
            }

        }
        private async void Btn_panggil2_Click(object sender, RoutedEventArgs e)
        {
            panggil(2);
            if (heap.IsEmpty != true)
            {
                txt_selanjutnya.Text = heap.GetMin().ToString();
            }
            else
            {
                txt_selanjutnya.Text = "....";
            }

        }

        //method buat masukin ke heap
        private int urut=1;
        private void antri(char tipe) //tipe P buat personal - B Bisnis
        {
            if(urut < 1000) //maksimal antrian <1000
            {
                if (tipe == 'P')
                {
                    string baru;
                    if (heap.IsEmpty == true & urut == 1)
                    {
                        heap.Tambah("P001");//masukin ke heap
                        MessageBox.Show("Nomor Antrian Anda P001");
                    }
                    else
                    {
                        urut++; //urut +1
                        if (urut < 10)
                        {
                            baru = "P00" + urut;
                        }
                        else if (urut < 100)
                        {
                            baru = "P0" + urut;
                        }
                        else
                        {
                            baru = "P" + urut;
                        }

                        heap.Tambah(baru);//masukin ke heap
                        MessageBox.Show("Nomor Antrian Anda " + baru);
                    }
                }
                else
                {
                    string baru;
                    if (heap.IsEmpty == true & urut == 1)
                    {
                        heap.Tambah("B001");
                        MessageBox.Show("Nomor Antrian Anda B001");
                    }
                    else
                    {
                        urut++;
                        if (urut < 10)
                        {
                            baru = "B00" + urut;
                        }
                        else if (urut < 100)
                        {
                            baru = "B0" + urut;
                        }
                        else
                        {
                            baru = "B" + urut;
                        }

                        heap.Tambah(baru);
                        MessageBox.Show("Nomor Antrian Anda " + baru);
                    }
                }
            }
            else
            {
                MessageBox.Show("Antrian Penuh");
            }
            
        }
        private void Btn_personal_Click(object sender, RoutedEventArgs e)
        {
            antri('P');//antri personal

            if (heap.IsEmpty != true)
            {
                txt_selanjutnya.Text = heap.GetMin().ToString();
            }

        }

        private void Btn_bisnis_Click(object sender, RoutedEventArgs e)
        {
            antri('B');//antri bisnis
            if (heap.IsEmpty != true)
            {
                txt_selanjutnya.Text = heap.GetMin().ToString();
            }
        }

        private void Btn_close_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void DragMe(object sender, MouseButtonEventArgs e)
        {
            try
            {
                DragMove();
            }
            catch (Exception)
            {

            }
        }


        //Tambahan
        private void Btn_plus10_Click(object sender, RoutedEventArgs e)
        {
            if (urut < 1000)
            {
                string baru;
                if (heap.IsEmpty == true & urut == 1)
                {
                    urut = urut + 10;
                    heap.Tambah("P010");
                    MessageBox.Show("Nomor Antrian Anda P010");
                }
                else
                {
                    urut=urut+10;
                    if (urut < 10)
                    {
                        baru = "P00" + urut;
                    }
                    else if (urut < 100)
                    {
                        baru = "P0" + urut;
                    }
                    else
                    {
                        baru = "P" + urut;
                    }

                    heap.Tambah(baru);
                    MessageBox.Show("Nomor Antrian Anda " + baru);
                    if (heap.IsEmpty != true)
                    {
                        txt_selanjutnya.Text = heap.GetMin().ToString();
                    }
                }
            }
            else
            {
                MessageBox.Show("Antrian Penuh");
            }
            
        }

        private void Btn_plus20_Click(object sender, RoutedEventArgs e)
        {
            if (urut < 1000)
            {
                string baru;
                if (heap.IsEmpty == true & urut == 1)
                {
                    urut = urut + 20;
                    heap.Tambah("P020");
                    MessageBox.Show("Nomor Antrian Anda P020");
                }
                else
                {
                    urut = urut + 20;
                    if (urut < 10)
                    {
                        baru = "P00" + urut;
                    }
                    else if (urut < 100)
                    {
                        baru = "P0" + urut;
                    }
                    else
                    {
                        baru = "P" + urut;
                    }

                    heap.Tambah(baru);
                    MessageBox.Show("Nomor Antrian Anda " + baru);
                    if (heap.IsEmpty != true)
                    {
                        txt_selanjutnya.Text = heap.GetMin().ToString();
                    }
                }
            }
            else
            {
                MessageBox.Show("Antrian Penuh");
            }
            
        }

        private void Btn_plus50_Click(object sender, RoutedEventArgs e)
        {
            if (urut < 1000)
            {
                string baru;
                if (heap.IsEmpty == true & urut == 1)
                {
                    urut = urut + 50;
                    heap.Tambah("P050");
                    MessageBox.Show("Nomor Antrian Anda P050");
                }
                else
                {
                    urut = urut + 50;
                    if (urut < 10)
                    {
                        baru = "P00" + urut;
                    }
                    else if (urut < 100)
                    {
                        baru = "P0" + urut;
                    }
                    else
                    {
                        baru = "P" + urut;
                    }

                    heap.Tambah(baru);
                    MessageBox.Show("Nomor Antrian Anda " + baru);
                    if (heap.IsEmpty != true)
                    {
                        txt_selanjutnya.Text = heap.GetMin().ToString();
                    }
                }
            }
            else
            {
                MessageBox.Show("Antrian Penuh");
            }
            
        }

        private void Btn_tentang_Click(object sender, RoutedEventArgs e)
        {
            WindowTentang wt = new WindowTentang();
            btn_closenav.Command.Execute(null);
            wt.Show();
            

        }
    }
}
