If you need more details, please refer to the comments on my code or my algorithm on the google drive

Graph Class:

	Data Members:
	 private Dictionary<Vertex, LinkedList<Node>> adjacencyList;
	 private List<Vertex> vertices;
	  private List<Edge> edges;

	  Methods:
	  public Graph()
	  public bool AddVertex(Vertex V)
	  public int NumVertices()
	   public int NumEdges()
	  public List<Vertex> GetVertices()
	  public List<Edge> GetEdges()
	  public bool AddEdge(Edge E)
	  public bool RemoveVertex(Vertex V)
	  public bool RemoveEdge(Edge E)
	  public LinkedList<Node> GetList(Vertex V)
	  public Vertex FindVertex(string name)
	  public Edge FindEdge(Vertex startVertex, Vertex endVertex, string name)
	  public bool IsAdjacent(Vertex source, Vertex dest)


Node Class:

	Data Members:
	private Vertex V;
	private List<Edge> incidentEdges;

	Methods:
	public Node(Vertex V, Edge E)
	public Vertex GetVertex()
	public List<Edge> GetIncidentEdges()
	public void AddIncidentEdge(Edge E)
	public void RemoveIncidentEdge(Edge E)
	public void PrintContents()


Vertex Class: NOTE: THIS IS A DUMMY CLASS

	Data members:
	private string ID;

	Methods:
	public Vertex(string name)
	public string GetID()
	public override string ToString()


Edge Class: NOTE: THIS IS ALSO A DUMMY CLASS

	Data Members:
	private int weight
	private int edgeID
	private Vertex source
	private Vertex dest

	Methods:
	public Edge(string name, Vertex source, Vertex dest, int weight)
	public string GetID()
	public int GetWeight()
	public Vertex GetSource()
	public Vertex GetDest()
	public override string ToString()
