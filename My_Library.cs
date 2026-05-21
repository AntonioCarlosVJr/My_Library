using System; 
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Programa{
	
	class Programa {
		
		static void Main(string[] args){
			
			try {
				
					//string arquivo = "/home/antonio/Área de trabalho/Programação_C#/MyBooks/livros.csv";
				
					Console.Write("Digite o Caminho do arquivo CSV: ");
					string arquivo = Console.ReadLine();
				
					ValidaCaminho(arquivo);
					
					int opcao = 0; 
						
						
						while(opcao != 5){
							
							Console.WriteLine("ESCOLHA UMA DAS OPÇÕES ABAIXO: ");
							Console.WriteLine("1 - Cadastrar Livros ");
							Console.WriteLine("2 - Bucar Por Autor ");
							Console.WriteLine("3 - Em construção ");
							Console.WriteLine("4 - Relatórios ");
							Console.WriteLine("5 - Sair ");
							
							Console.Write("Escolha o Número da Opção: ");
							opcao = int.Parse(Console.ReadLine());
							
							//Opção 1
							
							if (opcao == 1){
								
								Console.WriteLine("Quantos Livros serão cadastrados?");
								int qtdCad = int.Parse(Console.ReadLine());
								
								for (int i = 1; i <= qtdCad; i++){
									
										Console.WriteLine($"{i}° Cadastro");
										
										Console.Write("Nome do Livro: ");
										string nome = Console.ReadLine();
										
										Console.Write("Autor do Livro: ");
										string autor = Console.ReadLine();
										
										Console.Write("Lido (Sim / Não): ");
										string lido = Console.ReadLine();
										
										Console.Write("Nota: ");
										int nota = int.Parse(Console.ReadLine());

										Console.Write("Tipo: ");
										EnumTipo tipo = Enum.Parse<EnumTipo>(Console.ReadLine());
										
										
										Livros x = new Livros(nome, autor, lido, nota, tipo, arquivo);
										
										Console.WriteLine(" ");								
									
									}
								}
								
						   //Opção 2
						   
						   if (opcao == 2){
										
										Console.Write("Digite o nome do Autor: ");
										string n = Console.ReadLine();
									
										
										LivrosService.BuscaPorAutor(arquivo, n);
									}
						   //Opção 4
								if (opcao == 4){
									
										LivrosService.Relatorio(arquivo);
										
									
									}
								
							
								
							}	
								

						}				
				
			
				catch (Exception ex){
					
						Console.WriteLine("O Erro detectado foi: " + ex);
										
				}			
			
			}
			
			//Validador do Caminho do Arquivo
			
			public static void ValidaCaminho(string arquivo){
				
					Console.WriteLine("O Caminho é: " + arquivo);
					
					if (File.Exists(arquivo)){
						
							Console.WriteLine("Arquivo Encontrado");
						
						
						} else { 
							
							throw new Exception("Arquivo não encontrado");
							
							
						}
				}
		}
		
		class Livros {
			
				public string Nome {get; set;}
				public string Autor {get; set;}
				public string Lido {get; set;}
				public int Nota {get; set;}				
				public string Arquivo {get; set;}
				public EnumTipo Tipo  {get; set;}
				
				
				public Livros(string nome, string autor, string lido, int nota, EnumTipo tipo, string arq){
						
						Nome = nome;
						Autor = autor;
						Lido = lido;
						Nota = nota;
						Tipo = tipo;
						Arquivo = arq;
					
						CadastrarLivro();
															
					}
					
					
					public Livros(string nome, string autor, string lido, int nota, EnumTipo tipo){
						
						Nome = nome;
						Autor = autor;
						Lido = lido;
						Nota = nota;
						Tipo = tipo;					
															
					}
			
				public void CadastrarLivro(){
					
						using (StreamWriter sw = new StreamWriter(Arquivo, true, Encoding.UTF8)){
							
								sw.Write($"{Nome.ToUpper()};{Autor.ToUpper()};{Lido.ToUpper()};{Nota};{Tipo}\n");
								Console.WriteLine($"{Nome} - Cadastrado com Sucesso");			
					
							
							}		
							
					
					}
			
				public override string ToString(){
					
					return $"{Nome} - {Autor} - Foi Lido: {Lido} - Nota: {Nota} | Tipo {Tipo}";
					
					
					
					}
			
			
			}
		
		public enum EnumTipo : int {
			
				LIVRO = 1,
				MANGÁ = 2,
				GRAPICNOVEL = 3, 
				EBOOK = 4

			}
		
		class LivrosService {
			
				
				
				public static void BuscaPorAutor(string arquivo, string nome){
					
						List<Livros> autor = new List<Livros>();
						string linha;
						
						using (StreamReader sr = new StreamReader(arquivo)){
							
								while ((linha = sr.ReadLine()) != null){
									
										string[] lista = linha.Split(";");
										
										//public Livros(string nome, string autor, string lido, int nota, EnumTipo tipo)
										
										
										int nota = 0;
										int.TryParse(lista[3], out nota);

										autor.Add(
												new Livros(
															lista[0],
															lista[1],
															lista[2],
															nota,
															Enum.Parse<EnumTipo>(lista[4])
															));
																	
									}
							
								var busca = autor.Where(l => l.Autor.ToUpper() == nome.ToUpper());
								int contador = 0;
								
								foreach (var res in busca){
									
										Console.WriteLine(res);
										contador++;
									
									}
								
								Console.WriteLine($"Quantidade de Resultados: {contador} ");
								Console.WriteLine(" ");
							
							
							}
					}
					
					public static void Relatorio(string arquivo){
							
							Console.WriteLine("-------------------");
							
							List<Livros> rel = new List<Livros>();
							string linha;
							
							using (StreamReader sr = new StreamReader(arquivo, Encoding.UTF8)){
									
									while ((linha = sr.ReadLine()) != null){
										
											string[] dados = linha.Split(";");
											
											int nota = 0;
											int.TryParse(dados[3], out nota);
											
											
											rel.Add(
												new Livros(
															dados[0],
															dados[1],
															dados[2],
															nota,
															Enum.Parse<EnumTipo>(dados[4])
															));										
										}
								
								
									int cTotal = 0;
									var total = rel.Select(t => t);
									
									foreach (var x in total){
										
										cTotal++;
										
										} 
								
									Console.WriteLine($"Total de Itens: {cTotal}");
								
								
									
									int tLidos = 0;
									var lidos = rel.Where(l => l.Lido == "SIM");
									
									foreach (var l in lidos){
										
											tLidos++;
										
										}
									Console.WriteLine($"Total de Itens lidos: {tLidos}");
								
									int tLivros = 0;
									var livros = rel.Where(x => x.Tipo.Equals(Enum.Parse<EnumTipo>("LIVRO")));
									
									foreach (var li in livros){
										
											tLivros++;
										
										}
									
									
									
									
									
									int tManga = 0;
									var manga = rel.Where(x => x.Tipo.Equals(Enum.Parse<EnumTipo>("MANGÁ")));
									
									foreach (var ma in manga){
										
											tManga++;
										
										}
									
									
									
									
									
									int tEbook = 0;
									var ebook = rel.Where(x => x.Tipo.Equals(Enum.Parse<EnumTipo>("EBOOK")));
										
										foreach (var eb in ebook){
											
											tEbook++;
											
											
											}
									
									
								
									int tGrapic = 0;
									var grapic = rel.Where(x => x.Tipo.Equals(Enum.Parse<EnumTipo>("GRAPICNOVEL")));
										
										foreach (var gr in grapic){
											
											tGrapic++;
											
											
											}
									
									Console.WriteLine($" Total de Livros: {tLivros}");
									Console.WriteLine($" Total de Ebooks: {tEbook}");
									Console.WriteLine($" Total de Manga: {tManga}");
									Console.WriteLine($" Total de Graphic Novel: {tGrapic}");
									Console.WriteLine("---------------------------------------");
									Console.WriteLine("      ");
									
									
								
																
								}
						
								
						
						
						
						
						
						}
					
					
					
			}
		
		
	}
