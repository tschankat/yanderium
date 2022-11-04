using UnityEngine;
using UnityEngine.PostProcessing;
using UnityEngine.SceneManagement;

public class QualityManagerScript : MonoBehaviour
{
	public AntialiasingAsPostEffect PostAliasing;
	public StudentManagerScript StudentManager;
	public PostProcessingBehaviour Obscurance;
	public SettingsScript Settings;
	public NemesisScript Nemesis;
	public YandereScript Yandere;
	public Bloom BloomEffect;
	public Light Sun;

	public ParticleSystem EastRomanceBlossoms;
	public ParticleSystem WestRomanceBlossoms;
	public ParticleSystem CorridorBlossoms;
	public ParticleSystem PlazaBlossoms;
	public ParticleSystem MythBlossoms;

	public ParticleSystem[] Fountains;
	public ParticleSystem[] Steam;

	public Renderer YandereHairRenderer;
	public Shader NewBodyShader;
	public Shader NewHairShader;

	public Shader Toon;
	public Shader ToonOutline;

	public Shader ToonOverlay;
	public Shader ToonOutlineOverlay;

	public Shader ToonOutlineRimLight;

	public BloomAndLensFlares ExperimentalBloomAndLensFlares;
	public DepthOfField34 ExperimentalDepthOfField34;
	public SSAOEffect ExperimentalSSAOEffect;

	public bool RimLightActive;
	public bool DoNothing;

	// [af] FPS values and strings for each FPS index.
	static readonly int[] FPSValues = new int[] { int.MaxValue, 30, 60, 120 };
	public static readonly string[] FPSStrings = new string[] { "Unlimited", "30", "60", "120" };

	public void Start()
	{
		if (SceneManager.GetActiveScene().name != SceneNames.SchoolScene)
		{
			this.DoNothing = true;
		}

		if (!DoNothing)
		{
			DepthOfField34[] DepthOfField34s = Camera.main.GetComponents<DepthOfField34>();
			this.ExperimentalDepthOfField34 = DepthOfField34s[1];
			this.ToggleExperiment();

			if (OptionGlobals.ParticleCount == 0)
			{
				OptionGlobals.ParticleCount = 3;
			}

			if (OptionGlobals.DrawDistance == 0)
			{
				OptionGlobals.DrawDistanceLimit = 350;
				OptionGlobals.DrawDistance = 350;
			}

			if (OptionGlobals.DisableFarAnimations == 0)
			{
				OptionGlobals.DisableFarAnimations = 10;
			}

			if (OptionGlobals.Sensitivity == 0)
			{
				OptionGlobals.Sensitivity = 3;
			}

			this.ToggleRun();
			this.UpdateFog();
			this.UpdateAnims();
			this.UpdateBloom();
			this.UpdateShadows();
			this.UpdateFPSIndex();
			this.UpdateParticles();
			this.UpdateObscurance();
			this.UpdatePostAliasing();
			this.UpdateDrawDistance();
			this.UpdateLowDetailStudents();

			this.Settings.ToggleBackground();

			if (!OptionGlobals.DepthOfField)
			{
				this.ToggleExperiment();
			}
		}
	}

	public void UpdateParticles()
	{
		if (OptionGlobals.ParticleCount > 3)
		{
			OptionGlobals.ParticleCount = 1;
		}
		else if (OptionGlobals.ParticleCount < 1)
		{
			OptionGlobals.ParticleCount = 3;
		}

		if (!DoNothing)
		{
			// [af] This is the (unintuitive) Unity 5.3 way of changing emission.
			ParticleSystem.EmissionModule eastRomanceBlossomsEmission = this.EastRomanceBlossoms.emission;
			ParticleSystem.EmissionModule westRomanceBlossomsEmission = this.WestRomanceBlossoms.emission;
			ParticleSystem.EmissionModule corridorBlossomsEmission = this.CorridorBlossoms.emission;
			ParticleSystem.EmissionModule plazaBlossomsEmission = this.PlazaBlossoms.emission;
			ParticleSystem.EmissionModule mythBlossomsEmission = this.MythBlossoms.emission;
			ParticleSystem.EmissionModule steam1Emission = this.Steam[1].emission;
			ParticleSystem.EmissionModule steam2Emission = this.Steam[2].emission;
			ParticleSystem.EmissionModule fountain1Emission = this.Fountains[1].emission;
			ParticleSystem.EmissionModule fountain2Emission = this.Fountains[2].emission;
			ParticleSystem.EmissionModule fountain3Emission = this.Fountains[3].emission;

			eastRomanceBlossomsEmission.enabled = true;
			westRomanceBlossomsEmission.enabled = true;
			corridorBlossomsEmission.enabled = true;
			plazaBlossomsEmission.enabled = true;
			mythBlossomsEmission.enabled = true;
			steam1Emission.enabled = true;
			steam2Emission.enabled = true;
			fountain1Emission.enabled = true;
			fountain2Emission.enabled = true;
			fountain3Emission.enabled = true;

			if (OptionGlobals.ParticleCount == 3)
			{
				eastRomanceBlossomsEmission.rateOverTime = 100.0f;
				westRomanceBlossomsEmission.rateOverTime = 100.0f;
				corridorBlossomsEmission.rateOverTime = 1000.0f;
				plazaBlossomsEmission.rateOverTime = 400.0f;
				mythBlossomsEmission.rateOverTime = 100.0f;
				steam1Emission.rateOverTime = 100.0f;
				steam2Emission.rateOverTime = 100.0f;
				fountain1Emission.rateOverTime = 100.0f;
				fountain2Emission.rateOverTime = 100.0f;
				fountain3Emission.rateOverTime = 100.0f;
			}
			else if (OptionGlobals.ParticleCount == 2)
			{
				eastRomanceBlossomsEmission.rateOverTime = 10.0f;
				westRomanceBlossomsEmission.rateOverTime = 10.0f;
				corridorBlossomsEmission.rateOverTime = 100.0f;
				plazaBlossomsEmission.rateOverTime = 40.0f;
				mythBlossomsEmission.rateOverTime = 10.0f;
				steam1Emission.rateOverTime = 10.0f;
				steam2Emission.rateOverTime = 10.0f;
				fountain1Emission.rateOverTime = 10.0f;
				fountain2Emission.rateOverTime = 10.0f;
				fountain3Emission.rateOverTime = 10.0f;
			}
			else if (OptionGlobals.ParticleCount == 1)
			{
				eastRomanceBlossomsEmission.enabled = false;
				westRomanceBlossomsEmission.enabled = false;
				corridorBlossomsEmission.enabled = false;
				plazaBlossomsEmission.enabled = false;
				mythBlossomsEmission.enabled = false;
				steam1Emission.enabled = false;
				steam2Emission.enabled = false;
				fountain1Emission.enabled = false;
				fountain2Emission.enabled = false;
				fountain3Emission.enabled = false;
			}
		}
	}

	public void UpdateOutlines()
	{
		if (!DoNothing)
		{
			// [af] Converted while loop to for loop.
			for (int ID = 1; ID < this.StudentManager.Students.Length; ID++)
			{
				StudentScript student = this.StudentManager.Students[ID];

				if (student != null)
				{
					// [af] Added ".gameObject." for C# compatibility.
					if (student.gameObject.activeInHierarchy)
					{
						if (OptionGlobals.DisableOutlines)
						{
							this.NewHairShader = this.Toon; //Shader.Find("Toon/Lighted");
							this.NewBodyShader = this.ToonOverlay; //Shader.Find("Toon/Lighted Overlay Multiple Textures");
						}
						else
						{
							this.NewHairShader = this.ToonOutline; //Shader.Find("Toon/Lighted Outline");
							this.NewBodyShader = this.ToonOutlineOverlay; //Shader.Find("Toon/Lighted Outline Overlay Multiple Textures");
						}

						// If it's a girl...
						if (!student.Male)
						{
							student.MyRenderer.materials[0].shader = this.NewBodyShader;
							student.MyRenderer.materials[1].shader = this.NewBodyShader;
							student.MyRenderer.materials[2].shader = this.NewBodyShader;

							student.Cosmetic.RightStockings[0].GetComponent<Renderer>().material.shader = this.NewBodyShader;
							student.Cosmetic.LeftStockings[0].GetComponent<Renderer>().material.shader = this.NewBodyShader;

							if (student.Club == ClubType.Bully)
							{
								student.Cosmetic.Bookbag.GetComponent<Renderer>().material.shader = this.NewHairShader;
								student.Cosmetic.LeftWristband.GetComponent<Renderer>().material.shader = this.NewHairShader;
								student.Cosmetic.RightWristband.GetComponent<Renderer>().material.shader = this.NewHairShader;
								student.Cosmetic.HoodieRenderer.GetComponent<Renderer>().material.shader = this.NewHairShader;
							}

							if (student.StudentID == 87)
							{
								student.Cosmetic.ScarfRenderer.material.shader = NewHairShader;
							}
							else if (student.StudentID == 90)
							{
								if (student.Cosmetic.TeacherAccessories[student.Cosmetic.Accessory] != null)
								{
									student.Cosmetic.TeacherAccessories[student.Cosmetic.Accessory].GetComponent<Renderer>().material.shader = NewHairShader;
								}

								if (student.MyRenderer.materials.Length == 4)
								{
									student.MyRenderer.materials[3].shader = this.NewBodyShader;
								}
							}
						}
						// If it's a boy...
						else
						{
							student.MyRenderer.materials[0].shader = this.NewHairShader;
							student.MyRenderer.materials[1].shader = this.NewHairShader;
							student.MyRenderer.materials[2].shader = this.NewBodyShader;
						}

						student.Armband.GetComponent<Renderer>().material.shader = NewHairShader;

						if (!student.Male)
						{
							if (!student.Teacher)
							{
	#if UNITY_EDITOR
								//Debug.Log("QualityManager is currently setting materials for the student with the ID of: " + ID);
	#endif

								// [af] Commented in JS code.
								/*
								if (student == null)
								{
									Debug.Log("The Student was null.");
								}

								if (student.Cosmetic == null)
								{
									Debug.Log("The CosmeticScript was null.");
								}

								if (student.Cosmetic.FemaleHairRenderers[student.Cosmetic.Hairstyle] == null)
								{
									Debug.Log("The renderer was null.");
								}

								if (student.Cosmetic.FemaleHairRenderers[student.Cosmetic.Hairstyle].material == null)
								{
									Debug.Log("The material was null.");
								}

								if (student.Cosmetic.FemaleHairRenderers[student.Cosmetic.Hairstyle].material.shader == null)
								{
									Debug.Log("The shader was null.");
								}
								*/

								if (student.Cosmetic.FemaleHairRenderers[student.Cosmetic.Hairstyle] != null)
								{
									if (student.Cosmetic.FemaleHairRenderers[student.Cosmetic.Hairstyle].materials.Length == 1)
									{
										student.Cosmetic.FemaleHairRenderers[student.Cosmetic.Hairstyle].material.shader = this.NewHairShader;
									}
									else
									{
										student.Cosmetic.FemaleHairRenderers[student.Cosmetic.Hairstyle].materials[0].shader = this.NewHairShader;
										student.Cosmetic.FemaleHairRenderers[student.Cosmetic.Hairstyle].materials[1].shader = this.NewHairShader;
									}
								}

								if (student.Cosmetic.Accessory > 0)
								{
									if (student.Cosmetic.FemaleAccessories[student.Cosmetic.Accessory].GetComponent<Renderer>() != null)
									{
										student.Cosmetic.FemaleAccessories[student.Cosmetic.Accessory].GetComponent<Renderer>().material.shader = this.NewHairShader;
									}
								}
							}
							else
							{
								//Debug.Log("I am a teacher updating my hair renderer's material, and my ID is " + student.StudentID);

								if (student.Cosmetic.TeacherHairRenderers[student.Cosmetic.Hairstyle] != null)
								{
									student.Cosmetic.TeacherHairRenderers[student.Cosmetic.Hairstyle].material.shader = this.NewHairShader;
								}

								if (student.Cosmetic.Accessory > 0)
								{
									// [af] Commented in JS code.
									//StudentManager.Students[ID].Cosmetic.TeacherAccessories[StudentManager.Students[ID].Cosmetic.Accessory].GetComponent(Renderer).material.shader = NewHairShader;
								}
							}
						}
						else
						{
							if (student.Cosmetic.Hairstyle > 0)
							{
								if (student.Cosmetic.MaleHairRenderers[student.Cosmetic.Hairstyle].materials.Length == 1)
								{
									student.Cosmetic.MaleHairRenderers[student.Cosmetic.Hairstyle].material.shader = this.NewHairShader;
								}
								else
								{
									student.Cosmetic.MaleHairRenderers[student.Cosmetic.Hairstyle].materials[0].shader = this.NewHairShader;
									student.Cosmetic.MaleHairRenderers[student.Cosmetic.Hairstyle].materials[1].shader = this.NewHairShader;
								}
							}

							if (student.Cosmetic.Accessory > 0)
							{
								Renderer AccessoryRenderer = student.Cosmetic.MaleAccessories[student.Cosmetic.Accessory].GetComponent<Renderer>();

								if (AccessoryRenderer != null)
								{
									AccessoryRenderer.material.shader = this.NewHairShader;
								}
							}
						}

						if (!student.Teacher)
						{
							if (student.Cosmetic.Club > ClubType.None && student.Cosmetic.Club != ClubType.Council
							&& student.Cosmetic.Club != ClubType.Bully && student.Cosmetic.Club != ClubType.Delinquent)
							{
								if (student.Cosmetic.ClubAccessories[(int)student.Cosmetic.Club] != null)
								{
									Renderer ClubAccessoryRenderer =
										student.Cosmetic.ClubAccessories[(int)student.Cosmetic.Club].GetComponent<Renderer>();

									if (ClubAccessoryRenderer != null)
									{
										ClubAccessoryRenderer.material.shader = this.NewHairShader;
									}
								}
							}
						}
					}
				}
			}

			this.Yandere.MyRenderer.materials[0].shader = this.NewBodyShader;
			this.Yandere.MyRenderer.materials[1].shader = this.NewBodyShader;
			this.Yandere.MyRenderer.materials[2].shader = this.NewBodyShader;

			// [af] Converted while loop to for loop.
			for (int ID = 1; ID < this.Yandere.Hairstyles.Length; ID++)
			{
				Renderer YandereRenderer = this.Yandere.Hairstyles[ID].GetComponent<Renderer>();

				if (YandereRenderer != null)
				{
					this.YandereHairRenderer.material.shader = this.NewHairShader;
					YandereRenderer.material.shader = this.NewHairShader;
				}
			}

			this.Nemesis.Cosmetic.MyRenderer.materials[0].shader = this.NewBodyShader;
			this.Nemesis.Cosmetic.MyRenderer.materials[1].shader = this.NewBodyShader;
			this.Nemesis.Cosmetic.MyRenderer.materials[2].shader = this.NewBodyShader;
			this.Nemesis.NemesisHair.GetComponent<Renderer>().material.shader = this.NewHairShader;
		}
	}

	public void UpdatePostAliasing()
	{
		if (!DoNothing)
		{
			// [af] Replaced if/else statement with boolean expression.
			this.PostAliasing.enabled = !OptionGlobals.DisablePostAliasing;
		}
	}

	public void UpdateBloom()
	{
		if (!DoNothing)
		{
			// [af] Replaced if/else statement with boolean expression.
			this.BloomEffect.enabled = !OptionGlobals.DisableBloom;
		}
	}

	public void UpdateLowDetailStudents()
	{
		if (OptionGlobals.LowDetailStudents > 10)
		{
			OptionGlobals.LowDetailStudents = 0;
		}
		else if (OptionGlobals.LowDetailStudents < 0)
		{
			OptionGlobals.LowDetailStudents = 10;
		}

		if (!DoNothing)
		{
			this.StudentManager.LowDetailThreshold = OptionGlobals.LowDetailStudents * 10;

			bool Enable = false;

			if (this.StudentManager.LowDetailThreshold > 0.0f)
			{
				Enable = true;
			}
			else
			{
				Enable = false;
			}

			int LowID = 1;

			while (LowID < 101)
			{
				if (this.StudentManager.Students[LowID] != null)
				{
					this.StudentManager.Students[LowID].LowPoly.enabled = Enable;

					if (Enable == false)
					{
						if (this.StudentManager.Students[LowID].LowPoly.MyMesh.enabled)
						{
							this.StudentManager.Students[LowID].LowPoly.MyMesh.enabled = false;
							this.StudentManager.Students[LowID].MyRenderer.enabled = true;
						}
					}
				}

				LowID++;
			}
		}
	}

	public void UpdateAnims()
	{
		if (OptionGlobals.DisableFarAnimations > 20)
		{
			OptionGlobals.DisableFarAnimations = 1;
		}
		else if (OptionGlobals.DisableFarAnimations < 1)
		{
			OptionGlobals.DisableFarAnimations = 20;
		}

		if (!DoNothing)
		{
			this.StudentManager.FarAnimThreshold = OptionGlobals.DisableFarAnimations * 5;

			bool Enable = false;

			if (this.StudentManager.FarAnimThreshold > 0.0f)
			{
				this.StudentManager.DisableFarAnims = true;
			}
			else
			{
				this.StudentManager.DisableFarAnims = false;
			}
		}
	}

	public void UpdateDrawDistance()
	{
		//Debug.Log ("Draw Distance is " + OptionGlobals.DrawDistance + " and the Limit is " + OptionGlobals.DrawDistanceLimit);

		if (OptionGlobals.DrawDistance > OptionGlobals.DrawDistanceLimit)
		{
			OptionGlobals.DrawDistance = 10;
		}
		else if (OptionGlobals.DrawDistance < 1)
		{
			OptionGlobals.DrawDistance = OptionGlobals.DrawDistanceLimit;
		}

		if (!DoNothing)
		{
			Camera.main.farClipPlane = OptionGlobals.DrawDistance;
			RenderSettings.fogEndDistance = OptionGlobals.DrawDistance;
			this.Yandere.Smartphone.farClipPlane = OptionGlobals.DrawDistance;
		}
	}

	public void UpdateFog()
	{
		if (!DoNothing)
		{
			if (!OptionGlobals.Fog)
			{
				this.Yandere.MainCamera.clearFlags = CameraClearFlags.Skybox;

				RenderSettings.fogMode = FogMode.Exponential;
			}
			else
			{
				this.Yandere.MainCamera.clearFlags = CameraClearFlags.SolidColor;

				RenderSettings.fogMode = FogMode.Linear;
				RenderSettings.fogEndDistance = OptionGlobals.DrawDistance;
			}
		}
	}

	public void UpdateShadows()
	{
		if (!DoNothing)
		{
			//Debug.Log("EnableShadows is: " + OptionGlobals.EnableShadows);

			// [af] Replaced if/else statement with ternary expression.
			this.Sun.shadows = OptionGlobals.EnableShadows ?
				LightShadows.Soft : LightShadows.None;

			//Debug.Log ("Sun.shadows is: " + this.Sun.shadows);
		}
	}

	public void ToggleRun()
	{
		if (!DoNothing)
		{
			this.Yandere.ToggleRun = OptionGlobals.ToggleRun;
		}
	}

	public void UpdateFPSIndex()
	{
		//if (!DoNothing)
		//{
			if (OptionGlobals.FPSIndex < 0)
			{
				OptionGlobals.FPSIndex = FPSValues.Length - 1;
			}
			else if (OptionGlobals.FPSIndex >= FPSValues.Length)
			{
				OptionGlobals.FPSIndex = 0;
			}

			// [af] Decide which FPS to set it to.
			Application.targetFrameRate = FPSValues[OptionGlobals.FPSIndex];
		//}
	}

	public void ToggleExperiment()
	{
		if (!DoNothing)
		{
			if (!this.ExperimentalBloomAndLensFlares.enabled)
			{
				this.ExperimentalBloomAndLensFlares.enabled = true;
				this.ExperimentalDepthOfField34.enabled = false;
				this.ExperimentalSSAOEffect.enabled = false;
				this.BloomEffect.enabled = true;
			}
			else
			{
				this.ExperimentalBloomAndLensFlares.enabled = false;
				this.ExperimentalDepthOfField34.enabled = false;
				this.ExperimentalSSAOEffect.enabled = false;
				this.BloomEffect.enabled = false;
			}
		}
	}

	public void RimLight()
	{
		if (!DoNothing)
		{
			if (!this.RimLightActive)
			{
				this.RimLightActive = true;

				// [af] Converted while loop to for loop.
				for (int ID = 1; ID < this.StudentManager.Students.Length; ID++)
				{
					StudentScript student = this.StudentManager.Students[ID];

					if (student != null)
					{
						// [af] Added ".gameObject." for C# compatibility.
						if (student.gameObject.activeInHierarchy)
						{
							this.NewHairShader = this.ToonOutlineRimLight;
							this.NewBodyShader = this.ToonOutlineRimLight;

							student.MyRenderer.materials[0].shader = this.ToonOutlineRimLight;
							student.MyRenderer.materials[1].shader = this.ToonOutlineRimLight;
							student.MyRenderer.materials[2].shader = this.ToonOutlineRimLight;

							AdjustRimLight(student.MyRenderer.materials[0]);
							AdjustRimLight(student.MyRenderer.materials[1]);
							AdjustRimLight(student.MyRenderer.materials[2]);

							if (!student.Male)
							{
								if (!student.Teacher)
								{
									if (student.Cosmetic.FemaleHairRenderers[student.Cosmetic.Hairstyle] != null)
									{
										if (student.Cosmetic.FemaleHairRenderers[student.Cosmetic.Hairstyle].materials.Length == 1)
										{
											student.Cosmetic.FemaleHairRenderers[student.Cosmetic.Hairstyle].material.shader = this.ToonOutlineRimLight;
											AdjustRimLight(student.Cosmetic.FemaleHairRenderers[student.Cosmetic.Hairstyle].material);
										}
										else
										{
											student.Cosmetic.FemaleHairRenderers[student.Cosmetic.Hairstyle].materials[0].shader = this.ToonOutlineRimLight;
											student.Cosmetic.FemaleHairRenderers[student.Cosmetic.Hairstyle].materials[1].shader = this.ToonOutlineRimLight;

											AdjustRimLight(student.Cosmetic.FemaleHairRenderers[student.Cosmetic.Hairstyle].materials[0]);
											AdjustRimLight(student.Cosmetic.FemaleHairRenderers[student.Cosmetic.Hairstyle].materials[1]);
										}
									}

									if (student.Cosmetic.Accessory > 0)
									{
										if (student.Cosmetic.FemaleAccessories[student.Cosmetic.Accessory]
											.GetComponent<Renderer>() != null)
										{
											student.Cosmetic.FemaleAccessories[student.Cosmetic.Accessory]
												.GetComponent<Renderer>().material.shader = this.ToonOutlineRimLight;

											AdjustRimLight(student.Cosmetic.FemaleAccessories[student.Cosmetic.Accessory]
												.GetComponent<Renderer>().material);
										}
									}
								}
								else
								{
									student.Cosmetic.TeacherHairRenderers[student.Cosmetic.Hairstyle]
										.material.shader = this.ToonOutlineRimLight;

									AdjustRimLight(student.Cosmetic.TeacherHairRenderers[student.Cosmetic.Hairstyle]
										.material);
								}
							}
							else
							{
								if (student.Cosmetic.Hairstyle > 0)
								{
									if (student.Cosmetic.MaleHairRenderers[student.Cosmetic.Hairstyle].materials.Length == 1)
									{
										student.Cosmetic.MaleHairRenderers[student.Cosmetic.Hairstyle].material.shader = this.ToonOutlineRimLight;
										AdjustRimLight(student.Cosmetic.MaleHairRenderers[student.Cosmetic.Hairstyle].material);
									}
									else
									{
										student.Cosmetic.MaleHairRenderers[student.Cosmetic.Hairstyle].materials[0].shader = this.ToonOutlineRimLight;
										student.Cosmetic.MaleHairRenderers[student.Cosmetic.Hairstyle].materials[1].shader = this.ToonOutlineRimLight;

										AdjustRimLight(student.Cosmetic.MaleHairRenderers[student.Cosmetic.Hairstyle].materials[0]);
										AdjustRimLight(student.Cosmetic.MaleHairRenderers[student.Cosmetic.Hairstyle].materials[1]);
									}
								}

								if (student.Cosmetic.Accessory > 0)
								{
									Renderer AccessoryRenderer =
										student.Cosmetic.MaleAccessories[student.Cosmetic.Accessory].GetComponent<Renderer>();

									if (AccessoryRenderer != null)
									{
										AccessoryRenderer.material.shader = this.ToonOutlineRimLight;
										AdjustRimLight(AccessoryRenderer.material);
									}
								}
							}

							if (!student.Teacher)
							{
								if (student.Cosmetic.Club > ClubType.None && student.Cosmetic.Club != ClubType.Council
								&& student.Cosmetic.Club != ClubType.Bully && student.Cosmetic.Club != ClubType.Delinquent)
								{
									//Debug.Log(student.StudentID);

									if (student.Cosmetic.ClubAccessories[(int)student.Cosmetic.Club] != null)
									{
										Renderer ClubAccessoryRenderer =
											student.Cosmetic.ClubAccessories[(int)student.Cosmetic.Club].GetComponent<Renderer>();

										if (ClubAccessoryRenderer != null)
										{
											ClubAccessoryRenderer.material.shader = this.ToonOutlineRimLight;
											AdjustRimLight(ClubAccessoryRenderer.material);
										}
									}
								}
							}
						}
					}
				}

				this.Yandere.MyRenderer.materials[0].shader = this.ToonOutlineRimLight;
				this.Yandere.MyRenderer.materials[1].shader = this.ToonOutlineRimLight;
				this.Yandere.MyRenderer.materials[2].shader = this.ToonOutlineRimLight;

				AdjustRimLight(this.Yandere.MyRenderer.materials[0]);
				AdjustRimLight(this.Yandere.MyRenderer.materials[1]);
				AdjustRimLight(this.Yandere.MyRenderer.materials[2]);

				// [af] Converted while loop to for loop.
				for (int ID = 1; ID < this.Yandere.Hairstyles.Length; ID++)
				{
					Renderer YandereRenderer = this.Yandere.Hairstyles[ID].GetComponent<Renderer>();

					if (YandereRenderer != null)
					{
						this.YandereHairRenderer.material.shader = this.ToonOutlineRimLight;
						YandereRenderer.material.shader = this.ToonOutlineRimLight;

						AdjustRimLight(this.YandereHairRenderer.material);
						AdjustRimLight(YandereRenderer.material);
					}
				}

				this.Nemesis.Cosmetic.MyRenderer.materials[0].shader = this.ToonOutlineRimLight;
				this.Nemesis.Cosmetic.MyRenderer.materials[1].shader = this.ToonOutlineRimLight;
				this.Nemesis.Cosmetic.MyRenderer.materials[2].shader = this.ToonOutlineRimLight;
				this.Nemesis.NemesisHair.GetComponent<Renderer>().material.shader = this.ToonOutlineRimLight;

				AdjustRimLight(this.Nemesis.Cosmetic.MyRenderer.materials[0]);
				AdjustRimLight(this.Nemesis.Cosmetic.MyRenderer.materials[1]);
				AdjustRimLight(this.Nemesis.Cosmetic.MyRenderer.materials[2]);
				AdjustRimLight(this.Nemesis.NemesisHair.GetComponent<Renderer>().material);
			}
			else
			{
				this.RimLightActive = false;

				this.UpdateOutlines();
			}
		}
	}

	public void UpdateObscurance()
	{
		if (!DoNothing)
		{
			Obscurance.enabled = !OptionGlobals.DisableObscurance;
		}
	}

	public void AdjustRimLight(Material mat)
	{
		if (!DoNothing)
		{
			mat.SetFloat("_RimLightIntensity", 5);
			mat.SetFloat("_RimCrisp", .5f);
			mat.SetFloat("_RimAdditive", .5f);
		}
	}
}