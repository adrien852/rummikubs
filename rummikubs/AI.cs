using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rummikubs
{
    class AI
    {
        //VARIABLES
        private List<Kub> handKubs { get; set; }
        private bool firstHand { get; set; }
        private int turn { get; set; }
        private bool isHard { get; set; }

        //CONSTRUCTOR
        public AI(bool is_hard)
        {
            handKubs = new List<Kub>();
            firstHand = true;
            turn = 0;
            isHard = false;
        }

        public void PlayTurn(List<Kub> drawKubs)
        {
            if (turn == 0) {
                DrawKubs(14, drawKubs);
            }
            if (firstHand) {
                List<List<Kub>> scoreKubs = new List<List<Kub>>();
                BestFirstTurn(scoreKubs);
                int handScore = 0;
                foreach(List<Kub> listKub in scoreKubs) {
                    foreach (Kub kub in listKub) {
                        handScore += kub.score;
                    }
                }
                if (true) {
                    Play(scoreKubs);
                    firstHand = false;
                }
                else {
                    foreach (List<Kub> listKub in scoreKubs) {
                        DrawKubs(listKub.Count, listKub); //PAS BON DU TOUT FAUT REVOIR COMMENT RECUPERER SI TOTAL != 30
                    }
                }
            }
            turn++;
            /*else
                if (handKubs.length != 0)
                foreach (kub in handKubs)
                    List<List<Kub>> scoreKubs;
                        best_turn(kub, handKubs, table_kubs);
                        if (scoreKubs.length > 0)
                            play(scoreKubs, table_kubs);
                        else
                            draw(1, handKubs, drawKubs);
	            else
		            return;*/
        }

        public void Play(List<List<Kub>> scoreKubs)
        {
            String hand = "Score : ";
            foreach (List<Kub> listKub in scoreKubs) {
                String combination = "Comb : ";
                foreach (Kub kub in listKub) {
                    combination += kub.score + kub.color + " ";
                }
                hand += combination + "\n";
            }
            Console.WriteLine(hand);
        }

        public void BestFirstTurn(List<List<Kub>> scoreKubs)
        {
            scoreKubs.Add(new List<Kub>());
            for(int i=0;i<handKubs.Count;i++) {
                BestOccur(scoreKubs[scoreKubs.Count - 1], handKubs[i], i);
                if (scoreKubs[scoreKubs.Count - 1].Count > 0) {
                    BestFirstTurn(scoreKubs);
                    break;
                }
                else {
                    BestSeq(scoreKubs[scoreKubs.Count - 1], handKubs[i], "front", i);
                    if (scoreKubs[scoreKubs.Count - 1].Count > 0) {
                        BestFirstTurn(scoreKubs);
                        break;
                    }
                }
            }
            if(scoreKubs[scoreKubs.Count - 1].Count == 0) {
                scoreKubs.RemoveAt(scoreKubs.Count - 1);
            }

        }

        public void BestOccur(List<Kub> scoreKubs, Kub occurKub, int i)
        {
            scoreKubs.Insert(0, occurKub);
            handKubs.Remove(occurKub);
            for (int j = 0; j < handKubs.Count; j++) {
                if (handKubs[j].score == occurKub.score) {
                    if (!AlreadyInOccur(handKubs[j], scoreKubs)) {
                        BestOccur(scoreKubs, handKubs[j], j);
                        break;
                    }
                }
            }

            if(scoreKubs.Count < 3) {
                scoreKubs.Remove(occurKub);
                handKubs.Insert(i, occurKub);
            }
        }

        public bool AlreadyInOccur(Kub kub, List<Kub> scoreKubs)
        {
            foreach (Kub scoreKub in scoreKubs) {
                if (scoreKub.color == kub.color) {
                    return true;
                }
            }
            return false;
        }

        public void BestSeq(List<Kub> scoreKubs, Kub occurKub, String position, int i)
        {
            if(position == "front") {
                scoreKubs.Insert(0, occurKub);
            }
            else {
                scoreKubs.Insert(scoreKubs.Count - 1, occurKub);
            }
            handKubs.Remove(occurKub);

            for (int j = 0; j < handKubs.Count; j++) {
                if (handKubs[j].color == occurKub.color) {
                    if(handKubs[j].score == scoreKubs[0].score - 1) {
                        BestSeq(scoreKubs, handKubs[j], "front", j);
                        break;
                    }
                    else if(handKubs[j].score == scoreKubs[scoreKubs.Count - 1].score + 1) {
                        BestSeq(scoreKubs, handKubs[j], "back", j);
                        break;
                    }
                }
            }

            if(scoreKubs.Count < 3) {
                scoreKubs.Remove(occurKub);
                handKubs.Insert(i, occurKub);
            }
        }

        public void DrawKubs(int amount, List<Kub> drawKubs)
        {
            for(int i=0;i<amount;i++) {
                handKubs.Insert(0,drawKubs[0]);
                drawKubs.RemoveAt(0);
            }
        }

        public void ShowHand()
        {
            String kubString = "AI = ";
            foreach (Kub kub in handKubs) {
                kubString += kub.score + kub.color + " ";
            }
            Console.WriteLine(kubString);
        }
    }
}
