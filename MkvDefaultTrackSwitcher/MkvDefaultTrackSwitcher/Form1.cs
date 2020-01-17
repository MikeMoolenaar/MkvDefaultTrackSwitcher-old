using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using WindowsFormsApp1;

namespace MKVdefaultTrackSwitcher
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Version version = Assembly.GetExecutingAssembly().GetName().Version;
            this.Text += $" V{version.Major}.{version.Minor}";
        }

        private MkvReader mkvReader;
        private void btnSelectFiles_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "MKV files (*.mkv)|*.mkv";
                openFileDialog.Multiselect = true;
                if (openFileDialog.ShowDialog() != DialogResult.OK) return;

                List<string> files = openFileDialog.FileNames.ToList();
                lstFiles.Items.Clear();
                files.ForEach(x => lstFiles.Items.Add(Path.GetFileName(x)));

                mkvReader = new MkvReader(files);

                // Set audio streams in audio combobox
                cmbAudio.Items.Clear();
                cmbAudio.Items.AddRange(mkvReader.lsAudioStreams.ToArray());
                cmbAudio.SelectedItem = mkvReader.lsAudioStreams.FirstOrDefault(x => x.disposition.@default == 1);
                cmbAudio.Enabled = true;

                // Set subtitle streams in subtitle combobox
                cmbSubtitle.Items.Clear();
                cmbSubtitle.Items.Add("Disable");
                cmbSubtitle.Items.AddRange(mkvReader.lsSubtitleStreams.ToArray());
                cmbSubtitle.SelectedItem = mkvReader.lsSubtitleStreams.FirstOrDefault(x => x.disposition.@default == 1);
                if (cmbSubtitle.SelectedItem is null) cmbSubtitle.SelectedIndex = 0;
                cmbSubtitle.Enabled = true;

                btnApply.Enabled = true;
                lblStatus.Text = "";
            }

        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            btnApply.Enabled = false;

            MkvWriter mkvWriter = new MkvWriter(mkvReader.lsMkvfile);
            int audioIndex = ((Stream) cmbAudio.SelectedItem).index;
            int subtitleIndex = cmbSubtitle.SelectedItem == "Disable" ? -1 : ((Stream) cmbSubtitle.SelectedItem).index;
            mkvWriter.ApplyDefaultTracks(audioIndex, subtitleIndex);

            lblStatus.Text = "Done!";
            btnApply.Enabled = true;
        }

        private void cmb_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblStatus.Text = "";
        }

        private void lstFiles_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (e.IsSelected) e.Item.Selected = false;
        }

        private void lblHelp_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.lblHelp.LinkVisited = true;
            Process.Start("https://github.com/MikeYaye/MkvDefaultTrackSwitcher");
        }
    }
}
