namespace VoronoiDiagram{

class VoronoiSite : Point2D {
  private char _siteLetter;
  private char _cellLetter;
  public VoronoiSite(int X, int Y, char siteLetter, char cellLetter) : base(X, Y) { 
    this._siteLetter = siteLetter;
    this._cellLetter = cellLetter;
  }

  public char getSiteLetter(){
    return this._siteLetter;
  }

  public char getCellLetter(){
    return this._cellLetter;
  }

  public override string ToString(){
    return base.ToString() + " - " + this._siteLetter;
  }

}
}