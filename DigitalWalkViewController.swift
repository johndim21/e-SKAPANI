//
//  DigitalWalkViewController.swift
//  
//
//  Created by Panagiotis Theodosiadis on 29/1/21.
//

import UIKit

class DigitalWalkViewController: BaseViewController, ViewModelable {
    
    // MARK: - IBOutlets
    
    @IBOutlet weak var moveToSightDetailsView: UIView! {
        didSet {
            moveToSightDetailsView.backgroundColor = UIColor(red: 0.03, green: 0.69, blue: 0.90, alpha: 1.00)
            moveToSightDetailsView.layer.cornerRadius = moveToSightDetailsView.frame.width / 2
            moveToSightDetailsView.transform = CGAffineTransform(translationX: 0, y: UIScreen.main.bounds.height)
        }
    }
    @IBOutlet weak var moveToSightDetailsButton: UIButton! {
        didSet {
            moveToSightDetailsButton.setTitle("", for: .normal)
            moveToSightDetailsButton.setBackgroundImage(UIImage(named: "double_arrow_icon"), for: .normal)
            moveToSightDetailsButton.imageView?.contentMode = .scaleAspectFill
            moveToSightDetailsButton.imageEdgeInsets = UIEdgeInsets(top: -10, left: -10, bottom: -10, right: -10)
            moveToSightDetailsButton.tintColor = .white
            moveToSightDetailsButton.backgroundColor = .clear
        }
    }
    @IBOutlet weak var moveToSightDetailsHelper: UIButton! {
        didSet {
            moveToSightDetailsHelper.setTitle("", for: .normal)
            moveToSightDetailsHelper.backgroundColor = .clear
        }
    }
    @IBOutlet weak var digitalWalkChooserView: DigitalWalkChooserView! {
        didSet {
            digitalWalkChooserView.delegate = self
            digitalWalkChooserView.personImageFrameView.transform = CGAffineTransform(translationX: -(UIScreen.main.bounds.width * 0.3), y: 0)
            digitalWalkChooserView.transform = CGAffineTransform(translationX: 0, y: UIScreen.main.bounds.height)
            digitalWalkChooserView.view.layer.cornerRadius = 10
        }
    }
    
    //MARK: - IBActions
    
    @IBAction func moveToSightDetailsButtonTap(_ sender: Any) {
        guard let currentSight = viewModel.route, let dashboardNavigation = self.navigationController as? DashboardNavigationController else { return }
        dashboardNavigation.coordinator?.showSightDetails(currentSight)
    }
    
    // MARK: - Properties
    
    var viewModel: DigitalWalkViewModel!
    var rotondaButton: UIButton = UIButton()
    var archOfGaleriusButton: UIButton = UIButton()
    var hippodromeButton: UIButton = UIButton()
    var octagonButton: UIButton = UIButton()
    var vasilikiButton: UIButton = UIButton()
    
    let transition = BottomToTopAnimator()
    
    // MARK: - Lifecycle
    
    override func viewDidLoad() {
        super.viewDidLoad()
        
        view.backgroundColor = .clear
        view.isOpaque = false
        
        archOfGaleriusButton.setImage(UIImage(named: "arch_of_galerius_button_icon"), for: .normal)
        archOfGaleriusButton.setTitle("", for: .normal)
        archOfGaleriusButton.layer.opacity = 0
        hippodromeButton.setImage(UIImage(named: "ippodromos_button_icon"), for: .normal)
        hippodromeButton.setTitle("", for: .normal)
        hippodromeButton.layer.opacity = 0
        octagonButton.setImage(UIImage(named: "oktagwno_button_icon"), for: .normal)
        octagonButton.setTitle("", for: .normal)
        octagonButton.layer.opacity = 0
        vasilikiButton.setImage(UIImage(named: "vasiliki_button_icon"), for: .normal)
        vasilikiButton.setTitle("", for: .normal)
        vasilikiButton.layer.opacity = 0
        
        rotondaButton = UIButton(type: .custom)
        rotondaButton.frame = CGRect(x: UIScreen.main.bounds.width * 0.12, y: UIScreen.main.bounds.height * 0.18, width: UIScreen.main.bounds.height * 0.06, height: UIScreen.main.bounds.height * 0.06)
        rotondaButton.setImage(UIImage(named: "rotonta_button_icon"), for: .normal)
        rotondaButton.setTitle("", for: .normal)
        rotondaButton.addTarget(self, action: #selector(updateSightsViews(sender:)), for: .touchUpInside)
        rotondaButton.layer.opacity = 0
        self.view.addSubview(rotondaButton)
        
        archOfGaleriusButton = UIButton(type: .custom)
        archOfGaleriusButton.frame = CGRect(x: UIScreen.main.bounds.width * 0.22, y: UIScreen.main.bounds.height * 0.23, width: UIScreen.main.bounds.height * 0.06, height: UIScreen.main.bounds.height * 0.06)
        archOfGaleriusButton.setImage(UIImage(named: "arch_of_galerius_button_icon"), for: .normal)
        archOfGaleriusButton.setTitle("", for: .normal)
        archOfGaleriusButton.addTarget(self, action: #selector(updateSightsViews(sender:)), for: .touchUpInside)
        archOfGaleriusButton.layer.opacity = 0
        self.view.addSubview(archOfGaleriusButton)
        
        hippodromeButton = UIButton(type: .custom)
        hippodromeButton.frame = CGRect(x: UIScreen.main.bounds.width * 0.55, y: UIScreen.main.bounds.height * 0.28, width: UIScreen.main.bounds.height * 0.06, height: UIScreen.main.bounds.height * 0.06)
        hippodromeButton.setImage(UIImage(named: "ippodromos_button_icon"), for: .normal)
        hippodromeButton.setTitle("", for: .normal)
        hippodromeButton.addTarget(self, action: #selector(updateSightsViews(sender:)), for: .touchUpInside)
        hippodromeButton.layer.opacity = 0
        self.view.addSubview(hippodromeButton)
        
        octagonButton = UIButton(type: .custom)
        octagonButton.frame = CGRect(x: UIScreen.main.bounds.width * 0.14, y: UIScreen.main.bounds.height * 0.3, width: UIScreen.main.bounds.height * 0.06, height: UIScreen.main.bounds.height * 0.06)
        octagonButton.setImage(UIImage(named: "oktagwno_button_icon"), for: .normal)
        octagonButton.setTitle("", for: .normal)
        octagonButton.addTarget(self, action: #selector(updateSightsViews(sender:)), for: .touchUpInside)
        octagonButton.layer.opacity = 0
        self.view.addSubview(octagonButton)
        
        vasilikiButton = UIButton(type: .custom)
        vasilikiButton.frame = CGRect(x: UIScreen.main.bounds.width * 0.2, y: UIScreen.main.bounds.height * 0.35, width: UIScreen.main.bounds.height * 0.06, height: UIScreen.main.bounds.height * 0.06)
        vasilikiButton.setImage(UIImage(named: "vasiliki_button_icon"), for: .normal)
        vasilikiButton.setTitle("", for: .normal)
        vasilikiButton.addTarget(self, action: #selector(updateSightsViews(sender:)), for: .touchUpInside)
        vasilikiButton.layer.opacity = 0
        self.view.addSubview(vasilikiButton)
        
    }
    
    override func viewWillAppear(_ animated: Bool) {
        super.viewWillAppear(animated)
        
        UIView.animate(withDuration: 0.5) {
            self.rotondaButton.layer.opacity = 1
            self.archOfGaleriusButton.layer.opacity = 1
            self.hippodromeButton.layer.opacity = 1
            self.octagonButton.layer.opacity = 1
            self.vasilikiButton.layer.opacity = 1
            self.digitalWalkChooserView.transform = CGAffineTransform(translationX: 0, y: 0)
        }
    }
    
    override func viewDidDisappear(_ animated: Bool) {
        super.viewDidDisappear(animated)
        self.moveToSightDetailsButton.transform = CGAffineTransform(scaleX: 1.0, y: 1.0)
    }
    
    override func viewDidAppear(_ animated: Bool) {
        super.viewDidAppear(animated)
        if !isMovingToParent  {
            if self.moveToSightDetailsView.transform == CGAffineTransform(translationX: 0, y: 0) {
                self.doubleArrowsAnimation()
            }
        }
    }
    
    // MARK: - Methods
    
    @objc
    func updateSightsViews(sender: UIButton) {
        setupView(sender)
    }
    
    func doubleArrowsAnimation() {
        self.moveToSightDetailsButton.transform = CGAffineTransform(scaleX: 1.0, y: 1.0)
        UIView.animate(withDuration: 2.0, delay: 0.0, options: [.repeat, .autoreverse]) {
            self.moveToSightDetailsButton.transform = CGAffineTransform(scaleX: 0.8, y: 0.8)
        }
    }
    
    func setupView(_ sender: UIButton) {
        if moveToSightDetailsView.transform != CGAffineTransform(translationX: 0, y: 0) {
            UIView.animate(withDuration: 0.75) {
                self.moveToSightDetailsView.transform = CGAffineTransform(translationX: 0, y: 0)
            }
        }
        self.doubleArrowsAnimation()
        
        switch sender {
        case self.rotondaButton, self.digitalWalkChooserView.rotontaButton:
            self.viewModel.setCurrentSight(.rotonda)
            self.digitalWalkChooserView.setupSightInfo(.rotonda, ANLocalizedString("rotunda_description_label", comment: ""))
            
        case self.archOfGaleriusButton, self.digitalWalkChooserView.archOfGaleriusButton:
            self.viewModel.setCurrentSight(.archOfGalerius)
            self.digitalWalkChooserView.setupSightInfo(.archOfGalerius, ANLocalizedString("arch_of_galerius_description_label", comment: ""))
            
        case self.hippodromeButton, self.digitalWalkChooserView.ippodromosButton:
            self.viewModel.setCurrentSight(.hippodrome)
            self.digitalWalkChooserView.setupSightInfo(.hippodrome, ANLocalizedString("hippodrome_description_label", comment: ""))
            
        case self.octagonButton, self.digitalWalkChooserView.octagonButton:
            self.viewModel.setCurrentSight(.octagon)
            self.digitalWalkChooserView.setupSightInfo(.octagon,
                                                       ANLocalizedString("octagon_description_label", comment: ""))
            
        case self.vasilikiButton, self.digitalWalkChooserView.vasilikiButton:
            self.viewModel.setCurrentSight(.vasiliki)
            self.digitalWalkChooserView.setupSightInfo(.vasiliki, ANLocalizedString("vasiliki_description_label", comment: ""))
            
        default:
            break
        }
    }
}

extension DigitalWalkViewController: Storyboardable {
    static var storyboardName: String = "DigitalWalk"
    static var storyboardIdentifier: String? = "DigitalWalkViewControllerIdentifier"
}

extension DigitalWalkViewController: DigitalWalkChooserViewDelegate {
    func setSightInfo(_ sender: UIButton) {
        setupView(sender)
    }
}

